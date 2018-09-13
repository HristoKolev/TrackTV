namespace TrackTv.WebServices.Infrastructure
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;

    using JWT;
    using JWT.Algorithms;
    using JWT.Serializers;

    using log4net;

    public class SessionService<T>
        where T : class
    {
        private ILog Log { get; }

        public SessionService(ILog log)
        {
            this.Log = log;
        }

        // Just to satisfy the API.
        // In reality when `X509Certificate2` is used the byte[] key is ignored.
        // The method checks it for NULL and for Length - therefore the length of 1.
        // ReSharper disable once StaticMemberInGenericType
        private static readonly byte[] DummyKeyArray = new byte[1];

        public T DecodeSession(string jwt)
        {
            try
            {
                var serializer = new JsonNetSerializer();
                var validator = new JwtValidator(serializer, new UtcDateTimeProvider());
                var decoder = new JwtDecoder(serializer, validator, new JwtBase64UrlEncoder(), new RSAlgorithmFactory(GetCertificate));

                return decoder.DecodeToObject<T>(jwt, DummyKeyArray, true);
            }
            catch (Exception ex)
            {
                this.Log.Error(ex);

                return null;
            }
        }

        public string HashPassword(string password)
        {
            using (var hash = new SHA512Managed())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(password);

                byte[] hashedBytes = hash.ComputeHash(inputBytes);

                string encodedString = Convert.ToBase64String(hashedBytes);

                return encodedString;
            }
        }

        public string SignSession(T session)
        {
            var encoder = new JwtEncoder(new RS256Algorithm(GetCertificate()), new JsonNetSerializer(), new JwtBase64UrlEncoder());

            return encoder.Encode(session, DummyKeyArray);
        }

        private static X509Certificate2 GetCertificate()
        {
            return new X509Certificate2(Path.Combine(Global.DataDirectory, "certificate.pfx"));
        }
    }

    public class SessionService : SessionService<PublicSessionModel>
    {
        public SessionService(ILog log)
            : base(log)
        {
        }
    }

    public class PublicSessionModel
    {
        public int ProfileID { get; set; }
    }
}