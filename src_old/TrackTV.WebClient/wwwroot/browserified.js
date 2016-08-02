(function e(t,n,r){function s(o,u){if(!n[o]){if(!t[o]){var a=typeof require=="function"&&require;if(!u&&a)return a(o,!0);if(i)return i(o,!0);var f=new Error("Cannot find module '"+o+"'");throw f.code="MODULE_NOT_FOUND",f}var l=n[o]={exports:{}};t[o][0].call(l.exports,function(e){var n=t[o][1][e];return s(n?n:e)},l,l.exports,e,t,n,r)}return n[o].exports}var i=typeof require=="function"&&require;for(var o=0;o<r.length;o++)s(r[o]);return s})({1:[function(require,module,exports){
(function () {
    'use strict';

    window.ngModules.filters
        .constant('s', require('underscore.string'));

}());
},{"underscore.string":26}],2:[function(require,module,exports){
var trim = require('./trim');
var decap = require('./decapitalize');

module.exports = function camelize(str, decapitalize) {
  str = trim(str).replace(/[-_\s]+(.)?/g, function(match, c) {
    return c ? c.toUpperCase() : "";
  });

  if (decapitalize === true) {
    return decap(str);
  } else {
    return str;
  }
};

},{"./decapitalize":11,"./trim":63}],3:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function capitalize(str, lowercaseRest) {
  str = makeString(str);
  var remainingChars = !lowercaseRest ? str.slice(1) : str.slice(1).toLowerCase();

  return str.charAt(0).toUpperCase() + remainingChars;
};

},{"./helper/makeString":21}],4:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function chars(str) {
  return makeString(str).split('');
};

},{"./helper/makeString":21}],5:[function(require,module,exports){
module.exports = function chop(str, step) {
  if (str == null) return [];
  str = String(str);
  step = ~~step;
  return step > 0 ? str.match(new RegExp('.{1,' + step + '}', 'g')) : [str];
};

},{}],6:[function(require,module,exports){
var capitalize = require('./capitalize');
var camelize = require('./camelize');
var makeString = require('./helper/makeString');

module.exports = function classify(str) {
  str = makeString(str);
  return capitalize(camelize(str.replace(/[\W_]/g, ' ')).replace(/\s/g, ''));
};

},{"./camelize":2,"./capitalize":3,"./helper/makeString":21}],7:[function(require,module,exports){
var trim = require('./trim');

module.exports = function clean(str) {
  return trim(str).replace(/\s\s+/g, ' ');
};

},{"./trim":63}],8:[function(require,module,exports){

var makeString = require('./helper/makeString');

var from  = "ąàáäâãåæăćčĉęèéëêĝĥìíïîĵłľńňòóöőôõðøśșšŝťțŭùúüűûñÿýçżźž",
    to    = "aaaaaaaaaccceeeeeghiiiijllnnoooooooossssttuuuuuunyyczzz";

from += from.toUpperCase();
to += to.toUpperCase();

to = to.split("");

// for tokens requireing multitoken output
from += "ß";
to.push('ss');


module.exports = function cleanDiacritics(str) {
    return makeString(str).replace(/.{1}/g, function(c){
      var index = from.indexOf(c);
      return index === -1 ? c : to[index];
  });
};

},{"./helper/makeString":21}],9:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function(str, substr) {
  str = makeString(str);
  substr = makeString(substr);

  if (str.length === 0 || substr.length === 0) return 0;
  
  return str.split(substr).length - 1;
};

},{"./helper/makeString":21}],10:[function(require,module,exports){
var trim = require('./trim');

module.exports = function dasherize(str) {
  return trim(str).replace(/([A-Z])/g, '-$1').replace(/[-_\s]+/g, '-').toLowerCase();
};

},{"./trim":63}],11:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function decapitalize(str) {
  str = makeString(str);
  return str.charAt(0).toLowerCase() + str.slice(1);
};

},{"./helper/makeString":21}],12:[function(require,module,exports){
var makeString = require('./helper/makeString');

function getIndent(str) {
  var matches = str.match(/^[\s\\t]*/gm);
  var indent = matches[0].length;
  
  for (var i = 1; i < matches.length; i++) {
    indent = Math.min(matches[i].length, indent);
  }

  return indent;
}

module.exports = function dedent(str, pattern) {
  str = makeString(str);
  var indent = getIndent(str);
  var reg;

  if (indent === 0) return str;

  if (typeof pattern === 'string') {
    reg = new RegExp('^' + pattern, 'gm');
  } else {
    reg = new RegExp('^[ \\t]{' + indent + '}', 'gm');
  }

  return str.replace(reg, '');
};

},{"./helper/makeString":21}],13:[function(require,module,exports){
var makeString = require('./helper/makeString');
var toPositive = require('./helper/toPositive');

module.exports = function endsWith(str, ends, position) {
  str = makeString(str);
  ends = '' + ends;
  if (typeof position == 'undefined') {
    position = str.length - ends.length;
  } else {
    position = Math.min(toPositive(position), str.length) - ends.length;
  }
  return position >= 0 && str.indexOf(ends, position) === position;
};

},{"./helper/makeString":21,"./helper/toPositive":23}],14:[function(require,module,exports){
var makeString = require('./helper/makeString');
var escapeChars = require('./helper/escapeChars');
var reversedEscapeChars = {};

var regexString = "[";
for(var key in escapeChars) {
  regexString += key;
}
regexString += "]";

var regex = new RegExp( regexString, 'g');

module.exports = function escapeHTML(str) {

  return makeString(str).replace(regex, function(m) {
    return '&' + escapeChars[m] + ';';
  });
};

},{"./helper/escapeChars":18,"./helper/makeString":21}],15:[function(require,module,exports){
module.exports = function() {
  var result = {};

  for (var prop in this) {
    if (!this.hasOwnProperty(prop) || prop.match(/^(?:include|contains|reverse|join)$/)) continue;
    result[prop] = this[prop];
  }

  return result;
};

},{}],16:[function(require,module,exports){
var makeString = require('./makeString');

module.exports = function adjacent(str, direction) {
  str = makeString(str);
  if (str.length === 0) {
    return '';
  }
  return str.slice(0, -1) + String.fromCharCode(str.charCodeAt(str.length - 1) + direction);
};

},{"./makeString":21}],17:[function(require,module,exports){
var escapeRegExp = require('./escapeRegExp');

module.exports = function defaultToWhiteSpace(characters) {
  if (characters == null)
    return '\\s';
  else if (characters.source)
    return characters.source;
  else
    return '[' + escapeRegExp(characters) + ']';
};

},{"./escapeRegExp":19}],18:[function(require,module,exports){
/* We're explicitly defining the list of entities we want to escape.
nbsp is an HTML entity, but we don't want to escape all space characters in a string, hence its omission in this map.

*/
var escapeChars = {
  '¢' : 'cent',
  '£' : 'pound',
  '¥' : 'yen',
  '€': 'euro',
  '©' :'copy',
  '®' : 'reg',
  '<' : 'lt',
  '>' : 'gt',
  '"' : 'quot',
  '&' : 'amp',
  "'": '#39'
};

module.exports = escapeChars;

},{}],19:[function(require,module,exports){
var makeString = require('./makeString');

module.exports = function escapeRegExp(str) {
  return makeString(str).replace(/([.*+?^=!:${}()|[\]\/\\])/g, '\\$1');
};

},{"./makeString":21}],20:[function(require,module,exports){
/*
We're explicitly defining the list of entities that might see in escape HTML strings
*/
var htmlEntities = {
  nbsp: ' ',
  cent: '¢',
  pound: '£',
  yen: '¥',
  euro: '€',
  copy: '©',
  reg: '®',
  lt: '<',
  gt: '>',
  quot: '"',
  amp: '&',
  apos: "'"
};

module.exports = htmlEntities;

},{}],21:[function(require,module,exports){
/**
 * Ensure some object is a coerced to a string
 **/
module.exports = function makeString(object) {
  if (object == null) return '';
  return '' + object;
};

},{}],22:[function(require,module,exports){
module.exports = function strRepeat(str, qty){
  if (qty < 1) return '';
  var result = '';
  while (qty > 0) {
    if (qty & 1) result += str;
    qty >>= 1, str += str;
  }
  return result;
};

},{}],23:[function(require,module,exports){
module.exports = function toPositive(number) {
  return number < 0 ? 0 : (+number || 0);
};

},{}],24:[function(require,module,exports){
var capitalize = require('./capitalize');
var underscored = require('./underscored');
var trim = require('./trim');

module.exports = function humanize(str) {
  return capitalize(trim(underscored(str).replace(/_id$/, '').replace(/_/g, ' ')));
};

},{"./capitalize":3,"./trim":63,"./underscored":65}],25:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function include(str, needle) {
  if (needle === '') return true;
  return makeString(str).indexOf(needle) !== -1;
};

},{"./helper/makeString":21}],26:[function(require,module,exports){
//  Underscore.string
//  (c) 2010 Esa-Matti Suuronen <esa-matti aet suuronen dot org>
//  Underscore.string is freely distributable under the terms of the MIT license.
//  Documentation: https://github.com/epeli/underscore.string
//  Some code is borrowed from MooTools and Alexandru Marasteanu.
//  Version '3.2.2'

'use strict';

function s(value) {
  /* jshint validthis: true */
  if (!(this instanceof s)) return new s(value);
  this._wrapped = value;
}

s.VERSION = '3.2.2';

s.isBlank          = require('./isBlank');
s.stripTags        = require('./stripTags');
s.capitalize       = require('./capitalize');
s.decapitalize     = require('./decapitalize');
s.chop             = require('./chop');
s.trim             = require('./trim');
s.clean            = require('./clean');
s.cleanDiacritics  = require('./cleanDiacritics');
s.count            = require('./count');
s.chars            = require('./chars');
s.swapCase         = require('./swapCase');
s.escapeHTML       = require('./escapeHTML');
s.unescapeHTML     = require('./unescapeHTML');
s.splice           = require('./splice');
s.insert           = require('./insert');
s.replaceAll       = require('./replaceAll');
s.include          = require('./include');
s.join             = require('./join');
s.lines            = require('./lines');
s.dedent           = require('./dedent');
s.reverse          = require('./reverse');
s.startsWith       = require('./startsWith');
s.endsWith         = require('./endsWith');
s.pred             = require('./pred');
s.succ             = require('./succ');
s.titleize         = require('./titleize');
s.camelize         = require('./camelize');
s.underscored      = require('./underscored');
s.dasherize        = require('./dasherize');
s.classify         = require('./classify');
s.humanize         = require('./humanize');
s.ltrim            = require('./ltrim');
s.rtrim            = require('./rtrim');
s.truncate         = require('./truncate');
s.prune            = require('./prune');
s.words            = require('./words');
s.pad              = require('./pad');
s.lpad             = require('./lpad');
s.rpad             = require('./rpad');
s.lrpad            = require('./lrpad');
s.sprintf          = require('./sprintf');
s.vsprintf         = require('./vsprintf');
s.toNumber         = require('./toNumber');
s.numberFormat     = require('./numberFormat');
s.strRight         = require('./strRight');
s.strRightBack     = require('./strRightBack');
s.strLeft          = require('./strLeft');
s.strLeftBack      = require('./strLeftBack');
s.toSentence       = require('./toSentence');
s.toSentenceSerial = require('./toSentenceSerial');
s.slugify          = require('./slugify');
s.surround         = require('./surround');
s.quote            = require('./quote');
s.unquote          = require('./unquote');
s.repeat           = require('./repeat');
s.naturalCmp       = require('./naturalCmp');
s.levenshtein      = require('./levenshtein');
s.toBoolean        = require('./toBoolean');
s.exports          = require('./exports');
s.escapeRegExp     = require('./helper/escapeRegExp');
s.wrap             = require('./wrap');

// Aliases
s.strip     = s.trim;
s.lstrip    = s.ltrim;
s.rstrip    = s.rtrim;
s.center    = s.lrpad;
s.rjust     = s.lpad;
s.ljust     = s.rpad;
s.contains  = s.include;
s.q         = s.quote;
s.toBool    = s.toBoolean;
s.camelcase = s.camelize;


// Implement chaining
s.prototype = {
  value: function value() {
    return this._wrapped;
  }
};

function fn2method(key, fn) {
    if (typeof fn !== "function") return;
    s.prototype[key] = function() {
      var args = [this._wrapped].concat(Array.prototype.slice.call(arguments));
      var res = fn.apply(null, args);
      // if the result is non-string stop the chain and return the value
      return typeof res === 'string' ? new s(res) : res;
    };
}

// Copy functions to instance methods for chaining
for (var key in s) fn2method(key, s[key]);

fn2method("tap", function tap(string, fn) {
  return fn(string);
});

function prototype2method(methodName) {
  fn2method(methodName, function(context) {
    var args = Array.prototype.slice.call(arguments, 1);
    return String.prototype[methodName].apply(context, args);
  });
}

var prototypeMethods = [
  "toUpperCase",
  "toLowerCase",
  "split",
  "replace",
  "slice",
  "substring",
  "substr",
  "concat"
];

for (var key in prototypeMethods) prototype2method(prototypeMethods[key]);


module.exports = s;

},{"./camelize":2,"./capitalize":3,"./chars":4,"./chop":5,"./classify":6,"./clean":7,"./cleanDiacritics":8,"./count":9,"./dasherize":10,"./decapitalize":11,"./dedent":12,"./endsWith":13,"./escapeHTML":14,"./exports":15,"./helper/escapeRegExp":19,"./humanize":24,"./include":25,"./insert":27,"./isBlank":28,"./join":29,"./levenshtein":30,"./lines":31,"./lpad":32,"./lrpad":33,"./ltrim":34,"./naturalCmp":35,"./numberFormat":36,"./pad":37,"./pred":38,"./prune":39,"./quote":40,"./repeat":41,"./replaceAll":42,"./reverse":43,"./rpad":44,"./rtrim":45,"./slugify":46,"./splice":47,"./sprintf":48,"./startsWith":49,"./strLeft":50,"./strLeftBack":51,"./strRight":52,"./strRightBack":53,"./stripTags":54,"./succ":55,"./surround":56,"./swapCase":57,"./titleize":58,"./toBoolean":59,"./toNumber":60,"./toSentence":61,"./toSentenceSerial":62,"./trim":63,"./truncate":64,"./underscored":65,"./unescapeHTML":66,"./unquote":67,"./vsprintf":68,"./words":69,"./wrap":70}],27:[function(require,module,exports){
var splice = require('./splice');

module.exports = function insert(str, i, substr) {
  return splice(str, i, 0, substr);
};

},{"./splice":47}],28:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function isBlank(str) {
  return (/^\s*$/).test(makeString(str));
};

},{"./helper/makeString":21}],29:[function(require,module,exports){
var makeString = require('./helper/makeString');
var slice = [].slice;

module.exports = function join() {
  var args = slice.call(arguments),
    separator = args.shift();

  return args.join(makeString(separator));
};

},{"./helper/makeString":21}],30:[function(require,module,exports){
var makeString = require('./helper/makeString');

/**
 * Based on the implementation here: https://github.com/hiddentao/fast-levenshtein
 */
module.exports = function levenshtein(str1, str2) {
  'use strict';
  str1 = makeString(str1);
  str2 = makeString(str2);

  // Short cut cases  
  if (str1 === str2) return 0;
  if (!str1 || !str2) return Math.max(str1.length, str2.length);

  // two rows
  var prevRow = new Array(str2.length + 1);

  // initialise previous row
  for (var i = 0; i < prevRow.length; ++i) {
    prevRow[i] = i;
  }

  // calculate current row distance from previous row
  for (i = 0; i < str1.length; ++i) {
    var nextCol = i + 1;

    for (var j = 0; j < str2.length; ++j) {
      var curCol = nextCol;

      // substution
      nextCol = prevRow[j] + ( (str1.charAt(i) === str2.charAt(j)) ? 0 : 1 );
      // insertion
      var tmp = curCol + 1;
      if (nextCol > tmp) {
        nextCol = tmp;
      }
      // deletion
      tmp = prevRow[j + 1] + 1;
      if (nextCol > tmp) {
        nextCol = tmp;
      }

      // copy current col value into previous (in preparation for next iteration)
      prevRow[j] = curCol;
    }

    // copy last col value into previous (in preparation for next iteration)
    prevRow[j] = nextCol;
  }

  return nextCol;
};

},{"./helper/makeString":21}],31:[function(require,module,exports){
module.exports = function lines(str) {
  if (str == null) return [];
  return String(str).split(/\r\n?|\n/);
};

},{}],32:[function(require,module,exports){
var pad = require('./pad');

module.exports = function lpad(str, length, padStr) {
  return pad(str, length, padStr);
};

},{"./pad":37}],33:[function(require,module,exports){
var pad = require('./pad');

module.exports = function lrpad(str, length, padStr) {
  return pad(str, length, padStr, 'both');
};

},{"./pad":37}],34:[function(require,module,exports){
var makeString = require('./helper/makeString');
var defaultToWhiteSpace = require('./helper/defaultToWhiteSpace');
var nativeTrimLeft = String.prototype.trimLeft;

module.exports = function ltrim(str, characters) {
  str = makeString(str);
  if (!characters && nativeTrimLeft) return nativeTrimLeft.call(str);
  characters = defaultToWhiteSpace(characters);
  return str.replace(new RegExp('^' + characters + '+'), '');
};

},{"./helper/defaultToWhiteSpace":17,"./helper/makeString":21}],35:[function(require,module,exports){
module.exports = function naturalCmp(str1, str2) {
  if (str1 == str2) return 0;
  if (!str1) return -1;
  if (!str2) return 1;

  var cmpRegex = /(\.\d+|\d+|\D+)/g,
    tokens1 = String(str1).match(cmpRegex),
    tokens2 = String(str2).match(cmpRegex),
    count = Math.min(tokens1.length, tokens2.length);

  for (var i = 0; i < count; i++) {
    var a = tokens1[i],
      b = tokens2[i];

    if (a !== b) {
      var num1 = +a;
      var num2 = +b;
      if (num1 === num1 && num2 === num2) {
        return num1 > num2 ? 1 : -1;
      }
      return a < b ? -1 : 1;
    }
  }

  if (tokens1.length != tokens2.length)
    return tokens1.length - tokens2.length;

  return str1 < str2 ? -1 : 1;
};

},{}],36:[function(require,module,exports){
module.exports = function numberFormat(number, dec, dsep, tsep) {
  if (isNaN(number) || number == null) return '';

  number = number.toFixed(~~dec);
  tsep = typeof tsep == 'string' ? tsep : ',';

  var parts = number.split('.'),
    fnums = parts[0],
    decimals = parts[1] ? (dsep || '.') + parts[1] : '';

  return fnums.replace(/(\d)(?=(?:\d{3})+$)/g, '$1' + tsep) + decimals;
};

},{}],37:[function(require,module,exports){
var makeString = require('./helper/makeString');
var strRepeat = require('./helper/strRepeat');

module.exports = function pad(str, length, padStr, type) {
  str = makeString(str);
  length = ~~length;

  var padlen = 0;

  if (!padStr)
    padStr = ' ';
  else if (padStr.length > 1)
    padStr = padStr.charAt(0);

  switch (type) {
    case 'right':
      padlen = length - str.length;
      return str + strRepeat(padStr, padlen);
    case 'both':
      padlen = length - str.length;
      return strRepeat(padStr, Math.ceil(padlen / 2)) + str + strRepeat(padStr, Math.floor(padlen / 2));
    default: // 'left'
      padlen = length - str.length;
      return strRepeat(padStr, padlen) + str;
  }
};

},{"./helper/makeString":21,"./helper/strRepeat":22}],38:[function(require,module,exports){
var adjacent = require('./helper/adjacent');

module.exports = function succ(str) {
  return adjacent(str, -1);
};

},{"./helper/adjacent":16}],39:[function(require,module,exports){
/**
 * _s.prune: a more elegant version of truncate
 * prune extra chars, never leaving a half-chopped word.
 * @author github.com/rwz
 */
var makeString = require('./helper/makeString');
var rtrim = require('./rtrim');

module.exports = function prune(str, length, pruneStr) {
  str = makeString(str);
  length = ~~length;
  pruneStr = pruneStr != null ? String(pruneStr) : '...';

  if (str.length <= length) return str;

  var tmpl = function(c) {
    return c.toUpperCase() !== c.toLowerCase() ? 'A' : ' ';
  },
    template = str.slice(0, length + 1).replace(/.(?=\W*\w*$)/g, tmpl); // 'Hello, world' -> 'HellAA AAAAA'

  if (template.slice(template.length - 2).match(/\w\w/))
    template = template.replace(/\s*\S+$/, '');
  else
    template = rtrim(template.slice(0, template.length - 1));

  return (template + pruneStr).length > str.length ? str : str.slice(0, template.length) + pruneStr;
};

},{"./helper/makeString":21,"./rtrim":45}],40:[function(require,module,exports){
var surround = require('./surround');

module.exports = function quote(str, quoteChar) {
  return surround(str, quoteChar || '"');
};

},{"./surround":56}],41:[function(require,module,exports){
var makeString = require('./helper/makeString');
var strRepeat = require('./helper/strRepeat');

module.exports = function repeat(str, qty, separator) {
  str = makeString(str);

  qty = ~~qty;

  // using faster implementation if separator is not needed;
  if (separator == null) return strRepeat(str, qty);

  // this one is about 300x slower in Google Chrome
  for (var repeat = []; qty > 0; repeat[--qty] = str) {}
  return repeat.join(separator);
};

},{"./helper/makeString":21,"./helper/strRepeat":22}],42:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function replaceAll(str, find, replace, ignorecase) {
  var flags = (ignorecase === true)?'gi':'g';
  var reg = new RegExp(find, flags);

  return makeString(str).replace(reg, replace);
};

},{"./helper/makeString":21}],43:[function(require,module,exports){
var chars = require('./chars');

module.exports = function reverse(str) {
  return chars(str).reverse().join('');
};

},{"./chars":4}],44:[function(require,module,exports){
var pad = require('./pad');

module.exports = function rpad(str, length, padStr) {
  return pad(str, length, padStr, 'right');
};

},{"./pad":37}],45:[function(require,module,exports){
var makeString = require('./helper/makeString');
var defaultToWhiteSpace = require('./helper/defaultToWhiteSpace');
var nativeTrimRight = String.prototype.trimRight;

module.exports = function rtrim(str, characters) {
  str = makeString(str);
  if (!characters && nativeTrimRight) return nativeTrimRight.call(str);
  characters = defaultToWhiteSpace(characters);
  return str.replace(new RegExp(characters + '+$'), '');
};

},{"./helper/defaultToWhiteSpace":17,"./helper/makeString":21}],46:[function(require,module,exports){
var makeString = require('./helper/makeString');
var defaultToWhiteSpace = require('./helper/defaultToWhiteSpace');
var trim = require('./trim');
var dasherize = require('./dasherize');
var cleanDiacritics = require("./cleanDiacritics");

module.exports = function slugify(str) {
  return trim(dasherize(cleanDiacritics(str).replace(/[^\w\s-]/g, '-').toLowerCase()), '-');
};

},{"./cleanDiacritics":8,"./dasherize":10,"./helper/defaultToWhiteSpace":17,"./helper/makeString":21,"./trim":63}],47:[function(require,module,exports){
var chars = require('./chars');

module.exports = function splice(str, i, howmany, substr) {
  var arr = chars(str);
  arr.splice(~~i, ~~howmany, substr);
  return arr.join('');
};

},{"./chars":4}],48:[function(require,module,exports){
// sprintf() for JavaScript 0.7-beta1
// http://www.diveintojavascript.com/projects/javascript-sprintf
//
// Copyright (c) Alexandru Marasteanu <alexaholic [at) gmail (dot] com>
// All rights reserved.
var strRepeat = require('./helper/strRepeat');
var toString = Object.prototype.toString;
var sprintf = (function() {
  function get_type(variable) {
    return toString.call(variable).slice(8, -1).toLowerCase();
  }

  var str_repeat = strRepeat;

  var str_format = function() {
    if (!str_format.cache.hasOwnProperty(arguments[0])) {
      str_format.cache[arguments[0]] = str_format.parse(arguments[0]);
    }
    return str_format.format.call(null, str_format.cache[arguments[0]], arguments);
  };

  str_format.format = function(parse_tree, argv) {
    var cursor = 1, tree_length = parse_tree.length, node_type = '', arg, output = [], i, k, match, pad, pad_character, pad_length;
    for (i = 0; i < tree_length; i++) {
      node_type = get_type(parse_tree[i]);
      if (node_type === 'string') {
        output.push(parse_tree[i]);
      }
      else if (node_type === 'array') {
        match = parse_tree[i]; // convenience purposes only
        if (match[2]) { // keyword argument
          arg = argv[cursor];
          for (k = 0; k < match[2].length; k++) {
            if (!arg.hasOwnProperty(match[2][k])) {
              throw new Error(sprintf('[_.sprintf] property "%s" does not exist', match[2][k]));
            }
            arg = arg[match[2][k]];
          }
        } else if (match[1]) { // positional argument (explicit)
          arg = argv[match[1]];
        }
        else { // positional argument (implicit)
          arg = argv[cursor++];
        }

        if (/[^s]/.test(match[8]) && (get_type(arg) != 'number')) {
          throw new Error(sprintf('[_.sprintf] expecting number but found %s', get_type(arg)));
        }
        switch (match[8]) {
          case 'b': arg = arg.toString(2); break;
          case 'c': arg = String.fromCharCode(arg); break;
          case 'd': arg = parseInt(arg, 10); break;
          case 'e': arg = match[7] ? arg.toExponential(match[7]) : arg.toExponential(); break;
          case 'f': arg = match[7] ? parseFloat(arg).toFixed(match[7]) : parseFloat(arg); break;
          case 'o': arg = arg.toString(8); break;
          case 's': arg = ((arg = String(arg)) && match[7] ? arg.substring(0, match[7]) : arg); break;
          case 'u': arg = Math.abs(arg); break;
          case 'x': arg = arg.toString(16); break;
          case 'X': arg = arg.toString(16).toUpperCase(); break;
        }
        arg = (/[def]/.test(match[8]) && match[3] && arg >= 0 ? '+'+ arg : arg);
        pad_character = match[4] ? match[4] == '0' ? '0' : match[4].charAt(1) : ' ';
        pad_length = match[6] - String(arg).length;
        pad = match[6] ? str_repeat(pad_character, pad_length) : '';
        output.push(match[5] ? arg + pad : pad + arg);
      }
    }
    return output.join('');
  };

  str_format.cache = {};

  str_format.parse = function(fmt) {
    var _fmt = fmt, match = [], parse_tree = [], arg_names = 0;
    while (_fmt) {
      if ((match = /^[^\x25]+/.exec(_fmt)) !== null) {
        parse_tree.push(match[0]);
      }
      else if ((match = /^\x25{2}/.exec(_fmt)) !== null) {
        parse_tree.push('%');
      }
      else if ((match = /^\x25(?:([1-9]\d*)\$|\(([^\)]+)\))?(\+)?(0|'[^$])?(-)?(\d+)?(?:\.(\d+))?([b-fosuxX])/.exec(_fmt)) !== null) {
        if (match[2]) {
          arg_names |= 1;
          var field_list = [], replacement_field = match[2], field_match = [];
          if ((field_match = /^([a-z_][a-z_\d]*)/i.exec(replacement_field)) !== null) {
            field_list.push(field_match[1]);
            while ((replacement_field = replacement_field.substring(field_match[0].length)) !== '') {
              if ((field_match = /^\.([a-z_][a-z_\d]*)/i.exec(replacement_field)) !== null) {
                field_list.push(field_match[1]);
              }
              else if ((field_match = /^\[(\d+)\]/.exec(replacement_field)) !== null) {
                field_list.push(field_match[1]);
              }
              else {
                throw new Error('[_.sprintf] huh?');
              }
            }
          }
          else {
            throw new Error('[_.sprintf] huh?');
          }
          match[2] = field_list;
        }
        else {
          arg_names |= 2;
        }
        if (arg_names === 3) {
          throw new Error('[_.sprintf] mixing positional and named placeholders is not (yet) supported');
        }
        parse_tree.push(match);
      }
      else {
        throw new Error('[_.sprintf] huh?');
      }
      _fmt = _fmt.substring(match[0].length);
    }
    return parse_tree;
  };

  return str_format;
})();

module.exports = sprintf;

},{"./helper/strRepeat":22}],49:[function(require,module,exports){
var makeString = require('./helper/makeString');
var toPositive = require('./helper/toPositive');

module.exports = function startsWith(str, starts, position) {
  str = makeString(str);
  starts = '' + starts;
  position = position == null ? 0 : Math.min(toPositive(position), str.length);
  return str.lastIndexOf(starts, position) === position;
};

},{"./helper/makeString":21,"./helper/toPositive":23}],50:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function strLeft(str, sep) {
  str = makeString(str);
  sep = makeString(sep);
  var pos = !sep ? -1 : str.indexOf(sep);
  return~ pos ? str.slice(0, pos) : str;
};

},{"./helper/makeString":21}],51:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function strLeftBack(str, sep) {
  str = makeString(str);
  sep = makeString(sep);
  var pos = str.lastIndexOf(sep);
  return~ pos ? str.slice(0, pos) : str;
};

},{"./helper/makeString":21}],52:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function strRight(str, sep) {
  str = makeString(str);
  sep = makeString(sep);
  var pos = !sep ? -1 : str.indexOf(sep);
  return~ pos ? str.slice(pos + sep.length, str.length) : str;
};

},{"./helper/makeString":21}],53:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function strRightBack(str, sep) {
  str = makeString(str);
  sep = makeString(sep);
  var pos = !sep ? -1 : str.lastIndexOf(sep);
  return~ pos ? str.slice(pos + sep.length, str.length) : str;
};

},{"./helper/makeString":21}],54:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function stripTags(str) {
  return makeString(str).replace(/<\/?[^>]+>/g, '');
};

},{"./helper/makeString":21}],55:[function(require,module,exports){
var adjacent = require('./helper/adjacent');

module.exports = function succ(str) {
  return adjacent(str, 1);
};

},{"./helper/adjacent":16}],56:[function(require,module,exports){
module.exports = function surround(str, wrapper) {
  return [wrapper, str, wrapper].join('');
};

},{}],57:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function swapCase(str) {
  return makeString(str).replace(/\S/g, function(c) {
    return c === c.toUpperCase() ? c.toLowerCase() : c.toUpperCase();
  });
};

},{"./helper/makeString":21}],58:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function titleize(str) {
  return makeString(str).toLowerCase().replace(/(?:^|\s|-)\S/g, function(c) {
    return c.toUpperCase();
  });
};

},{"./helper/makeString":21}],59:[function(require,module,exports){
var trim = require('./trim');

function boolMatch(s, matchers) {
  var i, matcher, down = s.toLowerCase();
  matchers = [].concat(matchers);
  for (i = 0; i < matchers.length; i += 1) {
    matcher = matchers[i];
    if (!matcher) continue;
    if (matcher.test && matcher.test(s)) return true;
    if (matcher.toLowerCase() === down) return true;
  }
}

module.exports = function toBoolean(str, trueValues, falseValues) {
  if (typeof str === "number") str = "" + str;
  if (typeof str !== "string") return !!str;
  str = trim(str);
  if (boolMatch(str, trueValues || ["true", "1"])) return true;
  if (boolMatch(str, falseValues || ["false", "0"])) return false;
};

},{"./trim":63}],60:[function(require,module,exports){
var trim = require('./trim');

module.exports = function toNumber(num, precision) {
  if (num == null) return 0;
  var factor = Math.pow(10, isFinite(precision) ? precision : 0);
  return Math.round(num * factor) / factor;
};

},{"./trim":63}],61:[function(require,module,exports){
var rtrim = require('./rtrim');

module.exports = function toSentence(array, separator, lastSeparator, serial) {
  separator = separator || ', ';
  lastSeparator = lastSeparator || ' and ';
  var a = array.slice(),
    lastMember = a.pop();

  if (array.length > 2 && serial) lastSeparator = rtrim(separator) + lastSeparator;

  return a.length ? a.join(separator) + lastSeparator + lastMember : lastMember;
};

},{"./rtrim":45}],62:[function(require,module,exports){
var toSentence = require('./toSentence');

module.exports = function toSentenceSerial(array, sep, lastSep) {
  return toSentence(array, sep, lastSep, true);
};

},{"./toSentence":61}],63:[function(require,module,exports){
var makeString = require('./helper/makeString');
var defaultToWhiteSpace = require('./helper/defaultToWhiteSpace');
var nativeTrim = String.prototype.trim;

module.exports = function trim(str, characters) {
  str = makeString(str);
  if (!characters && nativeTrim) return nativeTrim.call(str);
  characters = defaultToWhiteSpace(characters);
  return str.replace(new RegExp('^' + characters + '+|' + characters + '+$', 'g'), '');
};

},{"./helper/defaultToWhiteSpace":17,"./helper/makeString":21}],64:[function(require,module,exports){
var makeString = require('./helper/makeString');

module.exports = function truncate(str, length, truncateStr) {
  str = makeString(str);
  truncateStr = truncateStr || '...';
  length = ~~length;
  return str.length > length ? str.slice(0, length) + truncateStr : str;
};

},{"./helper/makeString":21}],65:[function(require,module,exports){
var trim = require('./trim');

module.exports = function underscored(str) {
  return trim(str).replace(/([a-z\d])([A-Z]+)/g, '$1_$2').replace(/[-\s]+/g, '_').toLowerCase();
};

},{"./trim":63}],66:[function(require,module,exports){
var makeString = require('./helper/makeString');
var htmlEntities = require('./helper/htmlEntities');

module.exports = function unescapeHTML(str) {
  return makeString(str).replace(/\&([^;]+);/g, function(entity, entityCode) {
    var match;

    if (entityCode in htmlEntities) {
      return htmlEntities[entityCode];
    } else if (match = entityCode.match(/^#x([\da-fA-F]+)$/)) {
      return String.fromCharCode(parseInt(match[1], 16));
    } else if (match = entityCode.match(/^#(\d+)$/)) {
      return String.fromCharCode(~~match[1]);
    } else {
      return entity;
    }
  });
};

},{"./helper/htmlEntities":20,"./helper/makeString":21}],67:[function(require,module,exports){
module.exports = function unquote(str, quoteChar) {
  quoteChar = quoteChar || '"';
  if (str[0] === quoteChar && str[str.length - 1] === quoteChar)
    return str.slice(1, str.length - 1);
  else return str;
};

},{}],68:[function(require,module,exports){
var sprintf = require('./sprintf');

module.exports = function vsprintf(fmt, argv) {
  argv.unshift(fmt);
  return sprintf.apply(null, argv);
};

},{"./sprintf":48}],69:[function(require,module,exports){
var isBlank = require('./isBlank');
var trim = require('./trim');

module.exports = function words(str, delimiter) {
  if (isBlank(str)) return [];
  return trim(str, delimiter).split(delimiter || /\s+/);
};

},{"./isBlank":28,"./trim":63}],70:[function(require,module,exports){
// Wrap
// wraps a string by a certain width

makeString = require('./helper/makeString');

module.exports = function wrap(str, options){
	str = makeString(str);

	options = options || {};

	width = options.width || 75;
	seperator = options.seperator || '\n';
	cut = options.cut || false;
	preserveSpaces = options.preserveSpaces || false;
	trailingSpaces = options.trailingSpaces || false;

	if(width <= 0){
		return str;
	}

	else if(!cut){

		words = str.split(" ");
		result = "";
		current_column = 0;

		while(words.length > 0){
			
			// if adding a space and the next word would cause this line to be longer than width...
			if(1 + words[0].length + current_column > width){
				//start a new line if this line is not already empty
				if(current_column > 0){
					// add a space at the end of the line is preserveSpaces is true
					if (preserveSpaces){
						result += ' ';
						current_column++;
					}
					// fill the rest of the line with spaces if trailingSpaces option is true
					else if(trailingSpaces){
						while(current_column < width){
							result += ' ';
							current_column++;
						}						
					}
					//start new line
					result += seperator;
					current_column = 0;
				}
			}

			// if not at the begining of the line, add a space in front of the word
			if(current_column > 0){
				result += " ";
				current_column++;
			}

			// tack on the next word, update current column, a pop words array
			result += words[0];
			current_column += words[0].length;
			words.shift();

		}

		// fill the rest of the line with spaces if trailingSpaces option is true
		if(trailingSpaces){
			while(current_column < width){
				result += ' ';
				current_column++;
			}						
		}

		return result;

	}

	else {

		index = 0;
		result = "";

		// walk through each character and add seperators where appropriate
		while(index < str.length){
			if(index % width == 0 && index > 0){
				result += seperator;
			}
			result += str.charAt(index);
			index++;
		}

		// fill the rest of the line with spaces if trailingSpaces option is true
		if(trailingSpaces){
			while(index % width > 0){
				result += ' ';
				index++;
			}						
		}
		
		return result;
	}
};
},{"./helper/makeString":21}]},{},[1])
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJzb3VyY2VzIjpbIm5vZGVfbW9kdWxlcy9icm93c2VyLXBhY2svX3ByZWx1ZGUuanMiLCJhcHAvZmlsdGVycy9ucG1Nb2R1bGVzLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2NhbWVsaXplLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2NhcGl0YWxpemUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvY2hhcnMuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvY2hvcC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9jbGFzc2lmeS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9jbGVhbi5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9jbGVhbkRpYWNyaXRpY3MuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvY291bnQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvZGFzaGVyaXplLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2RlY2FwaXRhbGl6ZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9kZWRlbnQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvZW5kc1dpdGguanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvZXNjYXBlSFRNTC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9leHBvcnRzLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2hlbHBlci9hZGphY2VudC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9oZWxwZXIvZGVmYXVsdFRvV2hpdGVTcGFjZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9oZWxwZXIvZXNjYXBlQ2hhcnMuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvaGVscGVyL2VzY2FwZVJlZ0V4cC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9oZWxwZXIvaHRtbEVudGl0aWVzLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2hlbHBlci9tYWtlU3RyaW5nLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2hlbHBlci9zdHJSZXBlYXQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvaGVscGVyL3RvUG9zaXRpdmUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvaHVtYW5pemUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvaW5jbHVkZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9pbmRleC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9pbnNlcnQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvaXNCbGFuay5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9qb2luLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2xldmVuc2h0ZWluLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2xpbmVzLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL2xwYWQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvbHJwYWQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvbHRyaW0uanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvbmF0dXJhbENtcC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9udW1iZXJGb3JtYXQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvcGFkLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3ByZWQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvcHJ1bmUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvcXVvdGUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvcmVwZWF0LmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3JlcGxhY2VBbGwuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvcmV2ZXJzZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9ycGFkLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3J0cmltLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3NsdWdpZnkuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvc3BsaWNlLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3NwcmludGYuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvc3RhcnRzV2l0aC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9zdHJMZWZ0LmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3N0ckxlZnRCYWNrLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3N0clJpZ2h0LmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3N0clJpZ2h0QmFjay5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9zdHJpcFRhZ3MuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvc3VjYy5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9zdXJyb3VuZC5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy9zd2FwQ2FzZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy90aXRsZWl6ZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy90b0Jvb2xlYW4uanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdG9OdW1iZXIuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdG9TZW50ZW5jZS5qcyIsIm5vZGVfbW9kdWxlcy91bmRlcnNjb3JlLnN0cmluZy90b1NlbnRlbmNlU2VyaWFsLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3RyaW0uanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdHJ1bmNhdGUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdW5kZXJzY29yZWQuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdW5lc2NhcGVIVE1MLmpzIiwibm9kZV9tb2R1bGVzL3VuZGVyc2NvcmUuc3RyaW5nL3VucXVvdGUuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvdnNwcmludGYuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvd29yZHMuanMiLCJub2RlX21vZHVsZXMvdW5kZXJzY29yZS5zdHJpbmcvd3JhcC5qcyJdLCJuYW1lcyI6W10sIm1hcHBpbmdzIjoiQUFBQTtBQ0FBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ05BO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNkQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ0xBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ05BO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNSQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUN0QkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNWQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUM1QkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNiQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNsQkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNWQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNUQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1ZBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDbkJBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ25CQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1BBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1RBO0FBQ0E7QUFDQTtBQUNBOztBQ0hBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDMUlBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDVEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNwREE7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNKQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ0xBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDVkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQzdCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNaQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDMUJBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUMzQkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ0xBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ2ZBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNSQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ0xBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDVkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDVEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNQQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQzVIQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNUQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1JBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNSQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUkE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ0xBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNMQTtBQUNBO0FBQ0E7QUFDQTs7QUNIQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1BBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ3BCQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1BBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1pBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNMQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ1ZBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTs7QUNSQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDTEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDbEJBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ05BO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBOztBQ05BO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7O0FDUEE7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0E7QUFDQTtBQUNBO0FBQ0EiLCJmaWxlIjoiZ2VuZXJhdGVkLmpzIiwic291cmNlUm9vdCI6IiIsInNvdXJjZXNDb250ZW50IjpbIihmdW5jdGlvbiBlKHQsbixyKXtmdW5jdGlvbiBzKG8sdSl7aWYoIW5bb10pe2lmKCF0W29dKXt2YXIgYT10eXBlb2YgcmVxdWlyZT09XCJmdW5jdGlvblwiJiZyZXF1aXJlO2lmKCF1JiZhKXJldHVybiBhKG8sITApO2lmKGkpcmV0dXJuIGkobywhMCk7dmFyIGY9bmV3IEVycm9yKFwiQ2Fubm90IGZpbmQgbW9kdWxlICdcIitvK1wiJ1wiKTt0aHJvdyBmLmNvZGU9XCJNT0RVTEVfTk9UX0ZPVU5EXCIsZn12YXIgbD1uW29dPXtleHBvcnRzOnt9fTt0W29dWzBdLmNhbGwobC5leHBvcnRzLGZ1bmN0aW9uKGUpe3ZhciBuPXRbb11bMV1bZV07cmV0dXJuIHMobj9uOmUpfSxsLGwuZXhwb3J0cyxlLHQsbixyKX1yZXR1cm4gbltvXS5leHBvcnRzfXZhciBpPXR5cGVvZiByZXF1aXJlPT1cImZ1bmN0aW9uXCImJnJlcXVpcmU7Zm9yKHZhciBvPTA7bzxyLmxlbmd0aDtvKyspcyhyW29dKTtyZXR1cm4gc30pIiwiKGZ1bmN0aW9uICgpIHtcclxuICAgICd1c2Ugc3RyaWN0JztcclxuXHJcbiAgICB3aW5kb3cubmdNb2R1bGVzLmZpbHRlcnNcclxuICAgICAgICAuY29uc3RhbnQoJ3MnLCByZXF1aXJlKCd1bmRlcnNjb3JlLnN0cmluZycpKTtcclxuXHJcbn0oKSk7IiwidmFyIHRyaW0gPSByZXF1aXJlKCcuL3RyaW0nKTtcbnZhciBkZWNhcCA9IHJlcXVpcmUoJy4vZGVjYXBpdGFsaXplJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gY2FtZWxpemUoc3RyLCBkZWNhcGl0YWxpemUpIHtcbiAgc3RyID0gdHJpbShzdHIpLnJlcGxhY2UoL1stX1xcc10rKC4pPy9nLCBmdW5jdGlvbihtYXRjaCwgYykge1xuICAgIHJldHVybiBjID8gYy50b1VwcGVyQ2FzZSgpIDogXCJcIjtcbiAgfSk7XG5cbiAgaWYgKGRlY2FwaXRhbGl6ZSA9PT0gdHJ1ZSkge1xuICAgIHJldHVybiBkZWNhcChzdHIpO1xuICB9IGVsc2Uge1xuICAgIHJldHVybiBzdHI7XG4gIH1cbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBjYXBpdGFsaXplKHN0ciwgbG93ZXJjYXNlUmVzdCkge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIHZhciByZW1haW5pbmdDaGFycyA9ICFsb3dlcmNhc2VSZXN0ID8gc3RyLnNsaWNlKDEpIDogc3RyLnNsaWNlKDEpLnRvTG93ZXJDYXNlKCk7XG5cbiAgcmV0dXJuIHN0ci5jaGFyQXQoMCkudG9VcHBlckNhc2UoKSArIHJlbWFpbmluZ0NoYXJzO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGNoYXJzKHN0cikge1xuICByZXR1cm4gbWFrZVN0cmluZyhzdHIpLnNwbGl0KCcnKTtcbn07XG4iLCJtb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGNob3Aoc3RyLCBzdGVwKSB7XG4gIGlmIChzdHIgPT0gbnVsbCkgcmV0dXJuIFtdO1xuICBzdHIgPSBTdHJpbmcoc3RyKTtcbiAgc3RlcCA9IH5+c3RlcDtcbiAgcmV0dXJuIHN0ZXAgPiAwID8gc3RyLm1hdGNoKG5ldyBSZWdFeHAoJy57MSwnICsgc3RlcCArICd9JywgJ2cnKSkgOiBbc3RyXTtcbn07XG4iLCJ2YXIgY2FwaXRhbGl6ZSA9IHJlcXVpcmUoJy4vY2FwaXRhbGl6ZScpO1xudmFyIGNhbWVsaXplID0gcmVxdWlyZSgnLi9jYW1lbGl6ZScpO1xudmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gY2xhc3NpZnkoc3RyKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgcmV0dXJuIGNhcGl0YWxpemUoY2FtZWxpemUoc3RyLnJlcGxhY2UoL1tcXFdfXS9nLCAnICcpKS5yZXBsYWNlKC9cXHMvZywgJycpKTtcbn07XG4iLCJ2YXIgdHJpbSA9IHJlcXVpcmUoJy4vdHJpbScpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGNsZWFuKHN0cikge1xuICByZXR1cm4gdHJpbShzdHIpLnJlcGxhY2UoL1xcc1xccysvZywgJyAnKTtcbn07XG4iLCJcbnZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xuXG52YXIgZnJvbSAgPSBcIsSFw6DDocOkw6LDo8Olw6bEg8SHxI3EicSZw6jDqcOrw6rEncSlw6zDrcOvw67EtcWCxL7FhMWIw7LDs8O2xZHDtMO1w7DDuMWbyJnFocWdxaXIm8Wtw7nDusO8xbHDu8Oxw7/DvcOnxbzFusW+XCIsXG4gICAgdG8gICAgPSBcImFhYWFhYWFhYWNjY2VlZWVlZ2hpaWlpamxsbm5vb29vb29vb3Nzc3N0dHV1dXV1dW55eWN6enpcIjtcblxuZnJvbSArPSBmcm9tLnRvVXBwZXJDYXNlKCk7XG50byArPSB0by50b1VwcGVyQ2FzZSgpO1xuXG50byA9IHRvLnNwbGl0KFwiXCIpO1xuXG4vLyBmb3IgdG9rZW5zIHJlcXVpcmVpbmcgbXVsdGl0b2tlbiBvdXRwdXRcbmZyb20gKz0gXCLDn1wiO1xudG8ucHVzaCgnc3MnKTtcblxuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGNsZWFuRGlhY3JpdGljcyhzdHIpIHtcbiAgICByZXR1cm4gbWFrZVN0cmluZyhzdHIpLnJlcGxhY2UoLy57MX0vZywgZnVuY3Rpb24oYyl7XG4gICAgICB2YXIgaW5kZXggPSBmcm9tLmluZGV4T2YoYyk7XG4gICAgICByZXR1cm4gaW5kZXggPT09IC0xID8gYyA6IHRvW2luZGV4XTtcbiAgfSk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24oc3RyLCBzdWJzdHIpIHtcbiAgc3RyID0gbWFrZVN0cmluZyhzdHIpO1xuICBzdWJzdHIgPSBtYWtlU3RyaW5nKHN1YnN0cik7XG5cbiAgaWYgKHN0ci5sZW5ndGggPT09IDAgfHwgc3Vic3RyLmxlbmd0aCA9PT0gMCkgcmV0dXJuIDA7XG4gIFxuICByZXR1cm4gc3RyLnNwbGl0KHN1YnN0cikubGVuZ3RoIC0gMTtcbn07XG4iLCJ2YXIgdHJpbSA9IHJlcXVpcmUoJy4vdHJpbScpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGRhc2hlcml6ZShzdHIpIHtcbiAgcmV0dXJuIHRyaW0oc3RyKS5yZXBsYWNlKC8oW0EtWl0pL2csICctJDEnKS5yZXBsYWNlKC9bLV9cXHNdKy9nLCAnLScpLnRvTG93ZXJDYXNlKCk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gZGVjYXBpdGFsaXplKHN0cikge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIHJldHVybiBzdHIuY2hhckF0KDApLnRvTG93ZXJDYXNlKCkgKyBzdHIuc2xpY2UoMSk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbmZ1bmN0aW9uIGdldEluZGVudChzdHIpIHtcbiAgdmFyIG1hdGNoZXMgPSBzdHIubWF0Y2goL15bXFxzXFxcXHRdKi9nbSk7XG4gIHZhciBpbmRlbnQgPSBtYXRjaGVzWzBdLmxlbmd0aDtcbiAgXG4gIGZvciAodmFyIGkgPSAxOyBpIDwgbWF0Y2hlcy5sZW5ndGg7IGkrKykge1xuICAgIGluZGVudCA9IE1hdGgubWluKG1hdGNoZXNbaV0ubGVuZ3RoLCBpbmRlbnQpO1xuICB9XG5cbiAgcmV0dXJuIGluZGVudDtcbn1cblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBkZWRlbnQoc3RyLCBwYXR0ZXJuKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgdmFyIGluZGVudCA9IGdldEluZGVudChzdHIpO1xuICB2YXIgcmVnO1xuXG4gIGlmIChpbmRlbnQgPT09IDApIHJldHVybiBzdHI7XG5cbiAgaWYgKHR5cGVvZiBwYXR0ZXJuID09PSAnc3RyaW5nJykge1xuICAgIHJlZyA9IG5ldyBSZWdFeHAoJ14nICsgcGF0dGVybiwgJ2dtJyk7XG4gIH0gZWxzZSB7XG4gICAgcmVnID0gbmV3IFJlZ0V4cCgnXlsgXFxcXHRdeycgKyBpbmRlbnQgKyAnfScsICdnbScpO1xuICB9XG5cbiAgcmV0dXJuIHN0ci5yZXBsYWNlKHJlZywgJycpO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xudmFyIHRvUG9zaXRpdmUgPSByZXF1aXJlKCcuL2hlbHBlci90b1Bvc2l0aXZlJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gZW5kc1dpdGgoc3RyLCBlbmRzLCBwb3NpdGlvbikge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGVuZHMgPSAnJyArIGVuZHM7XG4gIGlmICh0eXBlb2YgcG9zaXRpb24gPT0gJ3VuZGVmaW5lZCcpIHtcbiAgICBwb3NpdGlvbiA9IHN0ci5sZW5ndGggLSBlbmRzLmxlbmd0aDtcbiAgfSBlbHNlIHtcbiAgICBwb3NpdGlvbiA9IE1hdGgubWluKHRvUG9zaXRpdmUocG9zaXRpb24pLCBzdHIubGVuZ3RoKSAtIGVuZHMubGVuZ3RoO1xuICB9XG4gIHJldHVybiBwb3NpdGlvbiA+PSAwICYmIHN0ci5pbmRleE9mKGVuZHMsIHBvc2l0aW9uKSA9PT0gcG9zaXRpb247XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG52YXIgZXNjYXBlQ2hhcnMgPSByZXF1aXJlKCcuL2hlbHBlci9lc2NhcGVDaGFycycpO1xudmFyIHJldmVyc2VkRXNjYXBlQ2hhcnMgPSB7fTtcblxudmFyIHJlZ2V4U3RyaW5nID0gXCJbXCI7XG5mb3IodmFyIGtleSBpbiBlc2NhcGVDaGFycykge1xuICByZWdleFN0cmluZyArPSBrZXk7XG59XG5yZWdleFN0cmluZyArPSBcIl1cIjtcblxudmFyIHJlZ2V4ID0gbmV3IFJlZ0V4cCggcmVnZXhTdHJpbmcsICdnJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gZXNjYXBlSFRNTChzdHIpIHtcblxuICByZXR1cm4gbWFrZVN0cmluZyhzdHIpLnJlcGxhY2UocmVnZXgsIGZ1bmN0aW9uKG0pIHtcbiAgICByZXR1cm4gJyYnICsgZXNjYXBlQ2hhcnNbbV0gKyAnOyc7XG4gIH0pO1xufTtcbiIsIm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24oKSB7XG4gIHZhciByZXN1bHQgPSB7fTtcblxuICBmb3IgKHZhciBwcm9wIGluIHRoaXMpIHtcbiAgICBpZiAoIXRoaXMuaGFzT3duUHJvcGVydHkocHJvcCkgfHwgcHJvcC5tYXRjaCgvXig/OmluY2x1ZGV8Y29udGFpbnN8cmV2ZXJzZXxqb2luKSQvKSkgY29udGludWU7XG4gICAgcmVzdWx0W3Byb3BdID0gdGhpc1twcm9wXTtcbiAgfVxuXG4gIHJldHVybiByZXN1bHQ7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBhZGphY2VudChzdHIsIGRpcmVjdGlvbikge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGlmIChzdHIubGVuZ3RoID09PSAwKSB7XG4gICAgcmV0dXJuICcnO1xuICB9XG4gIHJldHVybiBzdHIuc2xpY2UoMCwgLTEpICsgU3RyaW5nLmZyb21DaGFyQ29kZShzdHIuY2hhckNvZGVBdChzdHIubGVuZ3RoIC0gMSkgKyBkaXJlY3Rpb24pO1xufTtcbiIsInZhciBlc2NhcGVSZWdFeHAgPSByZXF1aXJlKCcuL2VzY2FwZVJlZ0V4cCcpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGRlZmF1bHRUb1doaXRlU3BhY2UoY2hhcmFjdGVycykge1xuICBpZiAoY2hhcmFjdGVycyA9PSBudWxsKVxuICAgIHJldHVybiAnXFxcXHMnO1xuICBlbHNlIGlmIChjaGFyYWN0ZXJzLnNvdXJjZSlcbiAgICByZXR1cm4gY2hhcmFjdGVycy5zb3VyY2U7XG4gIGVsc2VcbiAgICByZXR1cm4gJ1snICsgZXNjYXBlUmVnRXhwKGNoYXJhY3RlcnMpICsgJ10nO1xufTtcbiIsIi8qIFdlJ3JlIGV4cGxpY2l0bHkgZGVmaW5pbmcgdGhlIGxpc3Qgb2YgZW50aXRpZXMgd2Ugd2FudCB0byBlc2NhcGUuXG5uYnNwIGlzIGFuIEhUTUwgZW50aXR5LCBidXQgd2UgZG9uJ3Qgd2FudCB0byBlc2NhcGUgYWxsIHNwYWNlIGNoYXJhY3RlcnMgaW4gYSBzdHJpbmcsIGhlbmNlIGl0cyBvbWlzc2lvbiBpbiB0aGlzIG1hcC5cblxuKi9cbnZhciBlc2NhcGVDaGFycyA9IHtcbiAgJ8KiJyA6ICdjZW50JyxcbiAgJ8KjJyA6ICdwb3VuZCcsXG4gICfCpScgOiAneWVuJyxcbiAgJ+KCrCc6ICdldXJvJyxcbiAgJ8KpJyA6J2NvcHknLFxuICAnwq4nIDogJ3JlZycsXG4gICc8JyA6ICdsdCcsXG4gICc+JyA6ICdndCcsXG4gICdcIicgOiAncXVvdCcsXG4gICcmJyA6ICdhbXAnLFxuICBcIidcIjogJyMzOSdcbn07XG5cbm1vZHVsZS5leHBvcnRzID0gZXNjYXBlQ2hhcnM7XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vbWFrZVN0cmluZycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGVzY2FwZVJlZ0V4cChzdHIpIHtcbiAgcmV0dXJuIG1ha2VTdHJpbmcoc3RyKS5yZXBsYWNlKC8oWy4qKz9ePSE6JHt9KCl8W1xcXVxcL1xcXFxdKS9nLCAnXFxcXCQxJyk7XG59O1xuIiwiLypcbldlJ3JlIGV4cGxpY2l0bHkgZGVmaW5pbmcgdGhlIGxpc3Qgb2YgZW50aXRpZXMgdGhhdCBtaWdodCBzZWUgaW4gZXNjYXBlIEhUTUwgc3RyaW5nc1xuKi9cbnZhciBodG1sRW50aXRpZXMgPSB7XG4gIG5ic3A6ICcgJyxcbiAgY2VudDogJ8KiJyxcbiAgcG91bmQ6ICfCoycsXG4gIHllbjogJ8KlJyxcbiAgZXVybzogJ+KCrCcsXG4gIGNvcHk6ICfCqScsXG4gIHJlZzogJ8KuJyxcbiAgbHQ6ICc8JyxcbiAgZ3Q6ICc+JyxcbiAgcXVvdDogJ1wiJyxcbiAgYW1wOiAnJicsXG4gIGFwb3M6IFwiJ1wiXG59O1xuXG5tb2R1bGUuZXhwb3J0cyA9IGh0bWxFbnRpdGllcztcbiIsIi8qKlxuICogRW5zdXJlIHNvbWUgb2JqZWN0IGlzIGEgY29lcmNlZCB0byBhIHN0cmluZ1xuICoqL1xubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBtYWtlU3RyaW5nKG9iamVjdCkge1xuICBpZiAob2JqZWN0ID09IG51bGwpIHJldHVybiAnJztcbiAgcmV0dXJuICcnICsgb2JqZWN0O1xufTtcbiIsIm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc3RyUmVwZWF0KHN0ciwgcXR5KXtcbiAgaWYgKHF0eSA8IDEpIHJldHVybiAnJztcbiAgdmFyIHJlc3VsdCA9ICcnO1xuICB3aGlsZSAocXR5ID4gMCkge1xuICAgIGlmIChxdHkgJiAxKSByZXN1bHQgKz0gc3RyO1xuICAgIHF0eSA+Pj0gMSwgc3RyICs9IHN0cjtcbiAgfVxuICByZXR1cm4gcmVzdWx0O1xufTtcbiIsIm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdG9Qb3NpdGl2ZShudW1iZXIpIHtcbiAgcmV0dXJuIG51bWJlciA8IDAgPyAwIDogKCtudW1iZXIgfHwgMCk7XG59O1xuIiwidmFyIGNhcGl0YWxpemUgPSByZXF1aXJlKCcuL2NhcGl0YWxpemUnKTtcbnZhciB1bmRlcnNjb3JlZCA9IHJlcXVpcmUoJy4vdW5kZXJzY29yZWQnKTtcbnZhciB0cmltID0gcmVxdWlyZSgnLi90cmltJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gaHVtYW5pemUoc3RyKSB7XG4gIHJldHVybiBjYXBpdGFsaXplKHRyaW0odW5kZXJzY29yZWQoc3RyKS5yZXBsYWNlKC9faWQkLywgJycpLnJlcGxhY2UoL18vZywgJyAnKSkpO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIGluY2x1ZGUoc3RyLCBuZWVkbGUpIHtcbiAgaWYgKG5lZWRsZSA9PT0gJycpIHJldHVybiB0cnVlO1xuICByZXR1cm4gbWFrZVN0cmluZyhzdHIpLmluZGV4T2YobmVlZGxlKSAhPT0gLTE7XG59O1xuIiwiLy8gIFVuZGVyc2NvcmUuc3RyaW5nXG4vLyAgKGMpIDIwMTAgRXNhLU1hdHRpIFN1dXJvbmVuIDxlc2EtbWF0dGkgYWV0IHN1dXJvbmVuIGRvdCBvcmc+XG4vLyAgVW5kZXJzY29yZS5zdHJpbmcgaXMgZnJlZWx5IGRpc3RyaWJ1dGFibGUgdW5kZXIgdGhlIHRlcm1zIG9mIHRoZSBNSVQgbGljZW5zZS5cbi8vICBEb2N1bWVudGF0aW9uOiBodHRwczovL2dpdGh1Yi5jb20vZXBlbGkvdW5kZXJzY29yZS5zdHJpbmdcbi8vICBTb21lIGNvZGUgaXMgYm9ycm93ZWQgZnJvbSBNb29Ub29scyBhbmQgQWxleGFuZHJ1IE1hcmFzdGVhbnUuXG4vLyAgVmVyc2lvbiAnMy4yLjInXG5cbid1c2Ugc3RyaWN0JztcblxuZnVuY3Rpb24gcyh2YWx1ZSkge1xuICAvKiBqc2hpbnQgdmFsaWR0aGlzOiB0cnVlICovXG4gIGlmICghKHRoaXMgaW5zdGFuY2VvZiBzKSkgcmV0dXJuIG5ldyBzKHZhbHVlKTtcbiAgdGhpcy5fd3JhcHBlZCA9IHZhbHVlO1xufVxuXG5zLlZFUlNJT04gPSAnMy4yLjInO1xuXG5zLmlzQmxhbmsgICAgICAgICAgPSByZXF1aXJlKCcuL2lzQmxhbmsnKTtcbnMuc3RyaXBUYWdzICAgICAgICA9IHJlcXVpcmUoJy4vc3RyaXBUYWdzJyk7XG5zLmNhcGl0YWxpemUgICAgICAgPSByZXF1aXJlKCcuL2NhcGl0YWxpemUnKTtcbnMuZGVjYXBpdGFsaXplICAgICA9IHJlcXVpcmUoJy4vZGVjYXBpdGFsaXplJyk7XG5zLmNob3AgICAgICAgICAgICAgPSByZXF1aXJlKCcuL2Nob3AnKTtcbnMudHJpbSAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vdHJpbScpO1xucy5jbGVhbiAgICAgICAgICAgID0gcmVxdWlyZSgnLi9jbGVhbicpO1xucy5jbGVhbkRpYWNyaXRpY3MgID0gcmVxdWlyZSgnLi9jbGVhbkRpYWNyaXRpY3MnKTtcbnMuY291bnQgICAgICAgICAgICA9IHJlcXVpcmUoJy4vY291bnQnKTtcbnMuY2hhcnMgICAgICAgICAgICA9IHJlcXVpcmUoJy4vY2hhcnMnKTtcbnMuc3dhcENhc2UgICAgICAgICA9IHJlcXVpcmUoJy4vc3dhcENhc2UnKTtcbnMuZXNjYXBlSFRNTCAgICAgICA9IHJlcXVpcmUoJy4vZXNjYXBlSFRNTCcpO1xucy51bmVzY2FwZUhUTUwgICAgID0gcmVxdWlyZSgnLi91bmVzY2FwZUhUTUwnKTtcbnMuc3BsaWNlICAgICAgICAgICA9IHJlcXVpcmUoJy4vc3BsaWNlJyk7XG5zLmluc2VydCAgICAgICAgICAgPSByZXF1aXJlKCcuL2luc2VydCcpO1xucy5yZXBsYWNlQWxsICAgICAgID0gcmVxdWlyZSgnLi9yZXBsYWNlQWxsJyk7XG5zLmluY2x1ZGUgICAgICAgICAgPSByZXF1aXJlKCcuL2luY2x1ZGUnKTtcbnMuam9pbiAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vam9pbicpO1xucy5saW5lcyAgICAgICAgICAgID0gcmVxdWlyZSgnLi9saW5lcycpO1xucy5kZWRlbnQgICAgICAgICAgID0gcmVxdWlyZSgnLi9kZWRlbnQnKTtcbnMucmV2ZXJzZSAgICAgICAgICA9IHJlcXVpcmUoJy4vcmV2ZXJzZScpO1xucy5zdGFydHNXaXRoICAgICAgID0gcmVxdWlyZSgnLi9zdGFydHNXaXRoJyk7XG5zLmVuZHNXaXRoICAgICAgICAgPSByZXF1aXJlKCcuL2VuZHNXaXRoJyk7XG5zLnByZWQgICAgICAgICAgICAgPSByZXF1aXJlKCcuL3ByZWQnKTtcbnMuc3VjYyAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vc3VjYycpO1xucy50aXRsZWl6ZSAgICAgICAgID0gcmVxdWlyZSgnLi90aXRsZWl6ZScpO1xucy5jYW1lbGl6ZSAgICAgICAgID0gcmVxdWlyZSgnLi9jYW1lbGl6ZScpO1xucy51bmRlcnNjb3JlZCAgICAgID0gcmVxdWlyZSgnLi91bmRlcnNjb3JlZCcpO1xucy5kYXNoZXJpemUgICAgICAgID0gcmVxdWlyZSgnLi9kYXNoZXJpemUnKTtcbnMuY2xhc3NpZnkgICAgICAgICA9IHJlcXVpcmUoJy4vY2xhc3NpZnknKTtcbnMuaHVtYW5pemUgICAgICAgICA9IHJlcXVpcmUoJy4vaHVtYW5pemUnKTtcbnMubHRyaW0gICAgICAgICAgICA9IHJlcXVpcmUoJy4vbHRyaW0nKTtcbnMucnRyaW0gICAgICAgICAgICA9IHJlcXVpcmUoJy4vcnRyaW0nKTtcbnMudHJ1bmNhdGUgICAgICAgICA9IHJlcXVpcmUoJy4vdHJ1bmNhdGUnKTtcbnMucHJ1bmUgICAgICAgICAgICA9IHJlcXVpcmUoJy4vcHJ1bmUnKTtcbnMud29yZHMgICAgICAgICAgICA9IHJlcXVpcmUoJy4vd29yZHMnKTtcbnMucGFkICAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vcGFkJyk7XG5zLmxwYWQgICAgICAgICAgICAgPSByZXF1aXJlKCcuL2xwYWQnKTtcbnMucnBhZCAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vcnBhZCcpO1xucy5scnBhZCAgICAgICAgICAgID0gcmVxdWlyZSgnLi9scnBhZCcpO1xucy5zcHJpbnRmICAgICAgICAgID0gcmVxdWlyZSgnLi9zcHJpbnRmJyk7XG5zLnZzcHJpbnRmICAgICAgICAgPSByZXF1aXJlKCcuL3ZzcHJpbnRmJyk7XG5zLnRvTnVtYmVyICAgICAgICAgPSByZXF1aXJlKCcuL3RvTnVtYmVyJyk7XG5zLm51bWJlckZvcm1hdCAgICAgPSByZXF1aXJlKCcuL251bWJlckZvcm1hdCcpO1xucy5zdHJSaWdodCAgICAgICAgID0gcmVxdWlyZSgnLi9zdHJSaWdodCcpO1xucy5zdHJSaWdodEJhY2sgICAgID0gcmVxdWlyZSgnLi9zdHJSaWdodEJhY2snKTtcbnMuc3RyTGVmdCAgICAgICAgICA9IHJlcXVpcmUoJy4vc3RyTGVmdCcpO1xucy5zdHJMZWZ0QmFjayAgICAgID0gcmVxdWlyZSgnLi9zdHJMZWZ0QmFjaycpO1xucy50b1NlbnRlbmNlICAgICAgID0gcmVxdWlyZSgnLi90b1NlbnRlbmNlJyk7XG5zLnRvU2VudGVuY2VTZXJpYWwgPSByZXF1aXJlKCcuL3RvU2VudGVuY2VTZXJpYWwnKTtcbnMuc2x1Z2lmeSAgICAgICAgICA9IHJlcXVpcmUoJy4vc2x1Z2lmeScpO1xucy5zdXJyb3VuZCAgICAgICAgID0gcmVxdWlyZSgnLi9zdXJyb3VuZCcpO1xucy5xdW90ZSAgICAgICAgICAgID0gcmVxdWlyZSgnLi9xdW90ZScpO1xucy51bnF1b3RlICAgICAgICAgID0gcmVxdWlyZSgnLi91bnF1b3RlJyk7XG5zLnJlcGVhdCAgICAgICAgICAgPSByZXF1aXJlKCcuL3JlcGVhdCcpO1xucy5uYXR1cmFsQ21wICAgICAgID0gcmVxdWlyZSgnLi9uYXR1cmFsQ21wJyk7XG5zLmxldmVuc2h0ZWluICAgICAgPSByZXF1aXJlKCcuL2xldmVuc2h0ZWluJyk7XG5zLnRvQm9vbGVhbiAgICAgICAgPSByZXF1aXJlKCcuL3RvQm9vbGVhbicpO1xucy5leHBvcnRzICAgICAgICAgID0gcmVxdWlyZSgnLi9leHBvcnRzJyk7XG5zLmVzY2FwZVJlZ0V4cCAgICAgPSByZXF1aXJlKCcuL2hlbHBlci9lc2NhcGVSZWdFeHAnKTtcbnMud3JhcCAgICAgICAgICAgICA9IHJlcXVpcmUoJy4vd3JhcCcpO1xuXG4vLyBBbGlhc2VzXG5zLnN0cmlwICAgICA9IHMudHJpbTtcbnMubHN0cmlwICAgID0gcy5sdHJpbTtcbnMucnN0cmlwICAgID0gcy5ydHJpbTtcbnMuY2VudGVyICAgID0gcy5scnBhZDtcbnMucmp1c3QgICAgID0gcy5scGFkO1xucy5sanVzdCAgICAgPSBzLnJwYWQ7XG5zLmNvbnRhaW5zICA9IHMuaW5jbHVkZTtcbnMucSAgICAgICAgID0gcy5xdW90ZTtcbnMudG9Cb29sICAgID0gcy50b0Jvb2xlYW47XG5zLmNhbWVsY2FzZSA9IHMuY2FtZWxpemU7XG5cblxuLy8gSW1wbGVtZW50IGNoYWluaW5nXG5zLnByb3RvdHlwZSA9IHtcbiAgdmFsdWU6IGZ1bmN0aW9uIHZhbHVlKCkge1xuICAgIHJldHVybiB0aGlzLl93cmFwcGVkO1xuICB9XG59O1xuXG5mdW5jdGlvbiBmbjJtZXRob2Qoa2V5LCBmbikge1xuICAgIGlmICh0eXBlb2YgZm4gIT09IFwiZnVuY3Rpb25cIikgcmV0dXJuO1xuICAgIHMucHJvdG90eXBlW2tleV0gPSBmdW5jdGlvbigpIHtcbiAgICAgIHZhciBhcmdzID0gW3RoaXMuX3dyYXBwZWRdLmNvbmNhdChBcnJheS5wcm90b3R5cGUuc2xpY2UuY2FsbChhcmd1bWVudHMpKTtcbiAgICAgIHZhciByZXMgPSBmbi5hcHBseShudWxsLCBhcmdzKTtcbiAgICAgIC8vIGlmIHRoZSByZXN1bHQgaXMgbm9uLXN0cmluZyBzdG9wIHRoZSBjaGFpbiBhbmQgcmV0dXJuIHRoZSB2YWx1ZVxuICAgICAgcmV0dXJuIHR5cGVvZiByZXMgPT09ICdzdHJpbmcnID8gbmV3IHMocmVzKSA6IHJlcztcbiAgICB9O1xufVxuXG4vLyBDb3B5IGZ1bmN0aW9ucyB0byBpbnN0YW5jZSBtZXRob2RzIGZvciBjaGFpbmluZ1xuZm9yICh2YXIga2V5IGluIHMpIGZuMm1ldGhvZChrZXksIHNba2V5XSk7XG5cbmZuMm1ldGhvZChcInRhcFwiLCBmdW5jdGlvbiB0YXAoc3RyaW5nLCBmbikge1xuICByZXR1cm4gZm4oc3RyaW5nKTtcbn0pO1xuXG5mdW5jdGlvbiBwcm90b3R5cGUybWV0aG9kKG1ldGhvZE5hbWUpIHtcbiAgZm4ybWV0aG9kKG1ldGhvZE5hbWUsIGZ1bmN0aW9uKGNvbnRleHQpIHtcbiAgICB2YXIgYXJncyA9IEFycmF5LnByb3RvdHlwZS5zbGljZS5jYWxsKGFyZ3VtZW50cywgMSk7XG4gICAgcmV0dXJuIFN0cmluZy5wcm90b3R5cGVbbWV0aG9kTmFtZV0uYXBwbHkoY29udGV4dCwgYXJncyk7XG4gIH0pO1xufVxuXG52YXIgcHJvdG90eXBlTWV0aG9kcyA9IFtcbiAgXCJ0b1VwcGVyQ2FzZVwiLFxuICBcInRvTG93ZXJDYXNlXCIsXG4gIFwic3BsaXRcIixcbiAgXCJyZXBsYWNlXCIsXG4gIFwic2xpY2VcIixcbiAgXCJzdWJzdHJpbmdcIixcbiAgXCJzdWJzdHJcIixcbiAgXCJjb25jYXRcIlxuXTtcblxuZm9yICh2YXIga2V5IGluIHByb3RvdHlwZU1ldGhvZHMpIHByb3RvdHlwZTJtZXRob2QocHJvdG90eXBlTWV0aG9kc1trZXldKTtcblxuXG5tb2R1bGUuZXhwb3J0cyA9IHM7XG4iLCJ2YXIgc3BsaWNlID0gcmVxdWlyZSgnLi9zcGxpY2UnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBpbnNlcnQoc3RyLCBpLCBzdWJzdHIpIHtcbiAgcmV0dXJuIHNwbGljZShzdHIsIGksIDAsIHN1YnN0cik7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gaXNCbGFuayhzdHIpIHtcbiAgcmV0dXJuICgvXlxccyokLykudGVzdChtYWtlU3RyaW5nKHN0cikpO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xudmFyIHNsaWNlID0gW10uc2xpY2U7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gam9pbigpIHtcbiAgdmFyIGFyZ3MgPSBzbGljZS5jYWxsKGFyZ3VtZW50cyksXG4gICAgc2VwYXJhdG9yID0gYXJncy5zaGlmdCgpO1xuXG4gIHJldHVybiBhcmdzLmpvaW4obWFrZVN0cmluZyhzZXBhcmF0b3IpKTtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxuLyoqXG4gKiBCYXNlZCBvbiB0aGUgaW1wbGVtZW50YXRpb24gaGVyZTogaHR0cHM6Ly9naXRodWIuY29tL2hpZGRlbnRhby9mYXN0LWxldmVuc2h0ZWluXG4gKi9cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gbGV2ZW5zaHRlaW4oc3RyMSwgc3RyMikge1xuICAndXNlIHN0cmljdCc7XG4gIHN0cjEgPSBtYWtlU3RyaW5nKHN0cjEpO1xuICBzdHIyID0gbWFrZVN0cmluZyhzdHIyKTtcblxuICAvLyBTaG9ydCBjdXQgY2FzZXMgIFxuICBpZiAoc3RyMSA9PT0gc3RyMikgcmV0dXJuIDA7XG4gIGlmICghc3RyMSB8fCAhc3RyMikgcmV0dXJuIE1hdGgubWF4KHN0cjEubGVuZ3RoLCBzdHIyLmxlbmd0aCk7XG5cbiAgLy8gdHdvIHJvd3NcbiAgdmFyIHByZXZSb3cgPSBuZXcgQXJyYXkoc3RyMi5sZW5ndGggKyAxKTtcblxuICAvLyBpbml0aWFsaXNlIHByZXZpb3VzIHJvd1xuICBmb3IgKHZhciBpID0gMDsgaSA8IHByZXZSb3cubGVuZ3RoOyArK2kpIHtcbiAgICBwcmV2Um93W2ldID0gaTtcbiAgfVxuXG4gIC8vIGNhbGN1bGF0ZSBjdXJyZW50IHJvdyBkaXN0YW5jZSBmcm9tIHByZXZpb3VzIHJvd1xuICBmb3IgKGkgPSAwOyBpIDwgc3RyMS5sZW5ndGg7ICsraSkge1xuICAgIHZhciBuZXh0Q29sID0gaSArIDE7XG5cbiAgICBmb3IgKHZhciBqID0gMDsgaiA8IHN0cjIubGVuZ3RoOyArK2opIHtcbiAgICAgIHZhciBjdXJDb2wgPSBuZXh0Q29sO1xuXG4gICAgICAvLyBzdWJzdHV0aW9uXG4gICAgICBuZXh0Q29sID0gcHJldlJvd1tqXSArICggKHN0cjEuY2hhckF0KGkpID09PSBzdHIyLmNoYXJBdChqKSkgPyAwIDogMSApO1xuICAgICAgLy8gaW5zZXJ0aW9uXG4gICAgICB2YXIgdG1wID0gY3VyQ29sICsgMTtcbiAgICAgIGlmIChuZXh0Q29sID4gdG1wKSB7XG4gICAgICAgIG5leHRDb2wgPSB0bXA7XG4gICAgICB9XG4gICAgICAvLyBkZWxldGlvblxuICAgICAgdG1wID0gcHJldlJvd1tqICsgMV0gKyAxO1xuICAgICAgaWYgKG5leHRDb2wgPiB0bXApIHtcbiAgICAgICAgbmV4dENvbCA9IHRtcDtcbiAgICAgIH1cblxuICAgICAgLy8gY29weSBjdXJyZW50IGNvbCB2YWx1ZSBpbnRvIHByZXZpb3VzIChpbiBwcmVwYXJhdGlvbiBmb3IgbmV4dCBpdGVyYXRpb24pXG4gICAgICBwcmV2Um93W2pdID0gY3VyQ29sO1xuICAgIH1cblxuICAgIC8vIGNvcHkgbGFzdCBjb2wgdmFsdWUgaW50byBwcmV2aW91cyAoaW4gcHJlcGFyYXRpb24gZm9yIG5leHQgaXRlcmF0aW9uKVxuICAgIHByZXZSb3dbal0gPSBuZXh0Q29sO1xuICB9XG5cbiAgcmV0dXJuIG5leHRDb2w7XG59O1xuIiwibW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBsaW5lcyhzdHIpIHtcbiAgaWYgKHN0ciA9PSBudWxsKSByZXR1cm4gW107XG4gIHJldHVybiBTdHJpbmcoc3RyKS5zcGxpdCgvXFxyXFxuP3xcXG4vKTtcbn07XG4iLCJ2YXIgcGFkID0gcmVxdWlyZSgnLi9wYWQnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBscGFkKHN0ciwgbGVuZ3RoLCBwYWRTdHIpIHtcbiAgcmV0dXJuIHBhZChzdHIsIGxlbmd0aCwgcGFkU3RyKTtcbn07XG4iLCJ2YXIgcGFkID0gcmVxdWlyZSgnLi9wYWQnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBscnBhZChzdHIsIGxlbmd0aCwgcGFkU3RyKSB7XG4gIHJldHVybiBwYWQoc3RyLCBsZW5ndGgsIHBhZFN0ciwgJ2JvdGgnKTtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcbnZhciBkZWZhdWx0VG9XaGl0ZVNwYWNlID0gcmVxdWlyZSgnLi9oZWxwZXIvZGVmYXVsdFRvV2hpdGVTcGFjZScpO1xudmFyIG5hdGl2ZVRyaW1MZWZ0ID0gU3RyaW5nLnByb3RvdHlwZS50cmltTGVmdDtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBsdHJpbShzdHIsIGNoYXJhY3RlcnMpIHtcbiAgc3RyID0gbWFrZVN0cmluZyhzdHIpO1xuICBpZiAoIWNoYXJhY3RlcnMgJiYgbmF0aXZlVHJpbUxlZnQpIHJldHVybiBuYXRpdmVUcmltTGVmdC5jYWxsKHN0cik7XG4gIGNoYXJhY3RlcnMgPSBkZWZhdWx0VG9XaGl0ZVNwYWNlKGNoYXJhY3RlcnMpO1xuICByZXR1cm4gc3RyLnJlcGxhY2UobmV3IFJlZ0V4cCgnXicgKyBjaGFyYWN0ZXJzICsgJysnKSwgJycpO1xufTtcbiIsIm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gbmF0dXJhbENtcChzdHIxLCBzdHIyKSB7XG4gIGlmIChzdHIxID09IHN0cjIpIHJldHVybiAwO1xuICBpZiAoIXN0cjEpIHJldHVybiAtMTtcbiAgaWYgKCFzdHIyKSByZXR1cm4gMTtcblxuICB2YXIgY21wUmVnZXggPSAvKFxcLlxcZCt8XFxkK3xcXEQrKS9nLFxuICAgIHRva2VuczEgPSBTdHJpbmcoc3RyMSkubWF0Y2goY21wUmVnZXgpLFxuICAgIHRva2VuczIgPSBTdHJpbmcoc3RyMikubWF0Y2goY21wUmVnZXgpLFxuICAgIGNvdW50ID0gTWF0aC5taW4odG9rZW5zMS5sZW5ndGgsIHRva2VuczIubGVuZ3RoKTtcblxuICBmb3IgKHZhciBpID0gMDsgaSA8IGNvdW50OyBpKyspIHtcbiAgICB2YXIgYSA9IHRva2VuczFbaV0sXG4gICAgICBiID0gdG9rZW5zMltpXTtcblxuICAgIGlmIChhICE9PSBiKSB7XG4gICAgICB2YXIgbnVtMSA9ICthO1xuICAgICAgdmFyIG51bTIgPSArYjtcbiAgICAgIGlmIChudW0xID09PSBudW0xICYmIG51bTIgPT09IG51bTIpIHtcbiAgICAgICAgcmV0dXJuIG51bTEgPiBudW0yID8gMSA6IC0xO1xuICAgICAgfVxuICAgICAgcmV0dXJuIGEgPCBiID8gLTEgOiAxO1xuICAgIH1cbiAgfVxuXG4gIGlmICh0b2tlbnMxLmxlbmd0aCAhPSB0b2tlbnMyLmxlbmd0aClcbiAgICByZXR1cm4gdG9rZW5zMS5sZW5ndGggLSB0b2tlbnMyLmxlbmd0aDtcblxuICByZXR1cm4gc3RyMSA8IHN0cjIgPyAtMSA6IDE7XG59O1xuIiwibW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBudW1iZXJGb3JtYXQobnVtYmVyLCBkZWMsIGRzZXAsIHRzZXApIHtcbiAgaWYgKGlzTmFOKG51bWJlcikgfHwgbnVtYmVyID09IG51bGwpIHJldHVybiAnJztcblxuICBudW1iZXIgPSBudW1iZXIudG9GaXhlZCh+fmRlYyk7XG4gIHRzZXAgPSB0eXBlb2YgdHNlcCA9PSAnc3RyaW5nJyA/IHRzZXAgOiAnLCc7XG5cbiAgdmFyIHBhcnRzID0gbnVtYmVyLnNwbGl0KCcuJyksXG4gICAgZm51bXMgPSBwYXJ0c1swXSxcbiAgICBkZWNpbWFscyA9IHBhcnRzWzFdID8gKGRzZXAgfHwgJy4nKSArIHBhcnRzWzFdIDogJyc7XG5cbiAgcmV0dXJuIGZudW1zLnJlcGxhY2UoLyhcXGQpKD89KD86XFxkezN9KSskKS9nLCAnJDEnICsgdHNlcCkgKyBkZWNpbWFscztcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcbnZhciBzdHJSZXBlYXQgPSByZXF1aXJlKCcuL2hlbHBlci9zdHJSZXBlYXQnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBwYWQoc3RyLCBsZW5ndGgsIHBhZFN0ciwgdHlwZSkge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGxlbmd0aCA9IH5+bGVuZ3RoO1xuXG4gIHZhciBwYWRsZW4gPSAwO1xuXG4gIGlmICghcGFkU3RyKVxuICAgIHBhZFN0ciA9ICcgJztcbiAgZWxzZSBpZiAocGFkU3RyLmxlbmd0aCA+IDEpXG4gICAgcGFkU3RyID0gcGFkU3RyLmNoYXJBdCgwKTtcblxuICBzd2l0Y2ggKHR5cGUpIHtcbiAgICBjYXNlICdyaWdodCc6XG4gICAgICBwYWRsZW4gPSBsZW5ndGggLSBzdHIubGVuZ3RoO1xuICAgICAgcmV0dXJuIHN0ciArIHN0clJlcGVhdChwYWRTdHIsIHBhZGxlbik7XG4gICAgY2FzZSAnYm90aCc6XG4gICAgICBwYWRsZW4gPSBsZW5ndGggLSBzdHIubGVuZ3RoO1xuICAgICAgcmV0dXJuIHN0clJlcGVhdChwYWRTdHIsIE1hdGguY2VpbChwYWRsZW4gLyAyKSkgKyBzdHIgKyBzdHJSZXBlYXQocGFkU3RyLCBNYXRoLmZsb29yKHBhZGxlbiAvIDIpKTtcbiAgICBkZWZhdWx0OiAvLyAnbGVmdCdcbiAgICAgIHBhZGxlbiA9IGxlbmd0aCAtIHN0ci5sZW5ndGg7XG4gICAgICByZXR1cm4gc3RyUmVwZWF0KHBhZFN0ciwgcGFkbGVuKSArIHN0cjtcbiAgfVxufTtcbiIsInZhciBhZGphY2VudCA9IHJlcXVpcmUoJy4vaGVscGVyL2FkamFjZW50Jyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc3VjYyhzdHIpIHtcbiAgcmV0dXJuIGFkamFjZW50KHN0ciwgLTEpO1xufTtcbiIsIi8qKlxuICogX3MucHJ1bmU6IGEgbW9yZSBlbGVnYW50IHZlcnNpb24gb2YgdHJ1bmNhdGVcbiAqIHBydW5lIGV4dHJhIGNoYXJzLCBuZXZlciBsZWF2aW5nIGEgaGFsZi1jaG9wcGVkIHdvcmQuXG4gKiBAYXV0aG9yIGdpdGh1Yi5jb20vcnd6XG4gKi9cbnZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xudmFyIHJ0cmltID0gcmVxdWlyZSgnLi9ydHJpbScpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHBydW5lKHN0ciwgbGVuZ3RoLCBwcnVuZVN0cikge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGxlbmd0aCA9IH5+bGVuZ3RoO1xuICBwcnVuZVN0ciA9IHBydW5lU3RyICE9IG51bGwgPyBTdHJpbmcocHJ1bmVTdHIpIDogJy4uLic7XG5cbiAgaWYgKHN0ci5sZW5ndGggPD0gbGVuZ3RoKSByZXR1cm4gc3RyO1xuXG4gIHZhciB0bXBsID0gZnVuY3Rpb24oYykge1xuICAgIHJldHVybiBjLnRvVXBwZXJDYXNlKCkgIT09IGMudG9Mb3dlckNhc2UoKSA/ICdBJyA6ICcgJztcbiAgfSxcbiAgICB0ZW1wbGF0ZSA9IHN0ci5zbGljZSgwLCBsZW5ndGggKyAxKS5yZXBsYWNlKC8uKD89XFxXKlxcdyokKS9nLCB0bXBsKTsgLy8gJ0hlbGxvLCB3b3JsZCcgLT4gJ0hlbGxBQSBBQUFBQSdcblxuICBpZiAodGVtcGxhdGUuc2xpY2UodGVtcGxhdGUubGVuZ3RoIC0gMikubWF0Y2goL1xcd1xcdy8pKVxuICAgIHRlbXBsYXRlID0gdGVtcGxhdGUucmVwbGFjZSgvXFxzKlxcUyskLywgJycpO1xuICBlbHNlXG4gICAgdGVtcGxhdGUgPSBydHJpbSh0ZW1wbGF0ZS5zbGljZSgwLCB0ZW1wbGF0ZS5sZW5ndGggLSAxKSk7XG5cbiAgcmV0dXJuICh0ZW1wbGF0ZSArIHBydW5lU3RyKS5sZW5ndGggPiBzdHIubGVuZ3RoID8gc3RyIDogc3RyLnNsaWNlKDAsIHRlbXBsYXRlLmxlbmd0aCkgKyBwcnVuZVN0cjtcbn07XG4iLCJ2YXIgc3Vycm91bmQgPSByZXF1aXJlKCcuL3N1cnJvdW5kJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gcXVvdGUoc3RyLCBxdW90ZUNoYXIpIHtcbiAgcmV0dXJuIHN1cnJvdW5kKHN0ciwgcXVvdGVDaGFyIHx8ICdcIicpO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xudmFyIHN0clJlcGVhdCA9IHJlcXVpcmUoJy4vaGVscGVyL3N0clJlcGVhdCcpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHJlcGVhdChzdHIsIHF0eSwgc2VwYXJhdG9yKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcblxuICBxdHkgPSB+fnF0eTtcblxuICAvLyB1c2luZyBmYXN0ZXIgaW1wbGVtZW50YXRpb24gaWYgc2VwYXJhdG9yIGlzIG5vdCBuZWVkZWQ7XG4gIGlmIChzZXBhcmF0b3IgPT0gbnVsbCkgcmV0dXJuIHN0clJlcGVhdChzdHIsIHF0eSk7XG5cbiAgLy8gdGhpcyBvbmUgaXMgYWJvdXQgMzAweCBzbG93ZXIgaW4gR29vZ2xlIENocm9tZVxuICBmb3IgKHZhciByZXBlYXQgPSBbXTsgcXR5ID4gMDsgcmVwZWF0Wy0tcXR5XSA9IHN0cikge31cbiAgcmV0dXJuIHJlcGVhdC5qb2luKHNlcGFyYXRvcik7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gcmVwbGFjZUFsbChzdHIsIGZpbmQsIHJlcGxhY2UsIGlnbm9yZWNhc2UpIHtcbiAgdmFyIGZsYWdzID0gKGlnbm9yZWNhc2UgPT09IHRydWUpPydnaSc6J2cnO1xuICB2YXIgcmVnID0gbmV3IFJlZ0V4cChmaW5kLCBmbGFncyk7XG5cbiAgcmV0dXJuIG1ha2VTdHJpbmcoc3RyKS5yZXBsYWNlKHJlZywgcmVwbGFjZSk7XG59O1xuIiwidmFyIGNoYXJzID0gcmVxdWlyZSgnLi9jaGFycycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHJldmVyc2Uoc3RyKSB7XG4gIHJldHVybiBjaGFycyhzdHIpLnJldmVyc2UoKS5qb2luKCcnKTtcbn07XG4iLCJ2YXIgcGFkID0gcmVxdWlyZSgnLi9wYWQnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBycGFkKHN0ciwgbGVuZ3RoLCBwYWRTdHIpIHtcbiAgcmV0dXJuIHBhZChzdHIsIGxlbmd0aCwgcGFkU3RyLCAncmlnaHQnKTtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcbnZhciBkZWZhdWx0VG9XaGl0ZVNwYWNlID0gcmVxdWlyZSgnLi9oZWxwZXIvZGVmYXVsdFRvV2hpdGVTcGFjZScpO1xudmFyIG5hdGl2ZVRyaW1SaWdodCA9IFN0cmluZy5wcm90b3R5cGUudHJpbVJpZ2h0O1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHJ0cmltKHN0ciwgY2hhcmFjdGVycykge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGlmICghY2hhcmFjdGVycyAmJiBuYXRpdmVUcmltUmlnaHQpIHJldHVybiBuYXRpdmVUcmltUmlnaHQuY2FsbChzdHIpO1xuICBjaGFyYWN0ZXJzID0gZGVmYXVsdFRvV2hpdGVTcGFjZShjaGFyYWN0ZXJzKTtcbiAgcmV0dXJuIHN0ci5yZXBsYWNlKG5ldyBSZWdFeHAoY2hhcmFjdGVycyArICcrJCcpLCAnJyk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG52YXIgZGVmYXVsdFRvV2hpdGVTcGFjZSA9IHJlcXVpcmUoJy4vaGVscGVyL2RlZmF1bHRUb1doaXRlU3BhY2UnKTtcbnZhciB0cmltID0gcmVxdWlyZSgnLi90cmltJyk7XG52YXIgZGFzaGVyaXplID0gcmVxdWlyZSgnLi9kYXNoZXJpemUnKTtcbnZhciBjbGVhbkRpYWNyaXRpY3MgPSByZXF1aXJlKFwiLi9jbGVhbkRpYWNyaXRpY3NcIik7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc2x1Z2lmeShzdHIpIHtcbiAgcmV0dXJuIHRyaW0oZGFzaGVyaXplKGNsZWFuRGlhY3JpdGljcyhzdHIpLnJlcGxhY2UoL1teXFx3XFxzLV0vZywgJy0nKS50b0xvd2VyQ2FzZSgpKSwgJy0nKTtcbn07XG4iLCJ2YXIgY2hhcnMgPSByZXF1aXJlKCcuL2NoYXJzJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc3BsaWNlKHN0ciwgaSwgaG93bWFueSwgc3Vic3RyKSB7XG4gIHZhciBhcnIgPSBjaGFycyhzdHIpO1xuICBhcnIuc3BsaWNlKH5+aSwgfn5ob3dtYW55LCBzdWJzdHIpO1xuICByZXR1cm4gYXJyLmpvaW4oJycpO1xufTtcbiIsIi8vIHNwcmludGYoKSBmb3IgSmF2YVNjcmlwdCAwLjctYmV0YTFcbi8vIGh0dHA6Ly93d3cuZGl2ZWludG9qYXZhc2NyaXB0LmNvbS9wcm9qZWN0cy9qYXZhc2NyaXB0LXNwcmludGZcbi8vXG4vLyBDb3B5cmlnaHQgKGMpIEFsZXhhbmRydSBNYXJhc3RlYW51IDxhbGV4YWhvbGljIFthdCkgZ21haWwgKGRvdF0gY29tPlxuLy8gQWxsIHJpZ2h0cyByZXNlcnZlZC5cbnZhciBzdHJSZXBlYXQgPSByZXF1aXJlKCcuL2hlbHBlci9zdHJSZXBlYXQnKTtcbnZhciB0b1N0cmluZyA9IE9iamVjdC5wcm90b3R5cGUudG9TdHJpbmc7XG52YXIgc3ByaW50ZiA9IChmdW5jdGlvbigpIHtcbiAgZnVuY3Rpb24gZ2V0X3R5cGUodmFyaWFibGUpIHtcbiAgICByZXR1cm4gdG9TdHJpbmcuY2FsbCh2YXJpYWJsZSkuc2xpY2UoOCwgLTEpLnRvTG93ZXJDYXNlKCk7XG4gIH1cblxuICB2YXIgc3RyX3JlcGVhdCA9IHN0clJlcGVhdDtcblxuICB2YXIgc3RyX2Zvcm1hdCA9IGZ1bmN0aW9uKCkge1xuICAgIGlmICghc3RyX2Zvcm1hdC5jYWNoZS5oYXNPd25Qcm9wZXJ0eShhcmd1bWVudHNbMF0pKSB7XG4gICAgICBzdHJfZm9ybWF0LmNhY2hlW2FyZ3VtZW50c1swXV0gPSBzdHJfZm9ybWF0LnBhcnNlKGFyZ3VtZW50c1swXSk7XG4gICAgfVxuICAgIHJldHVybiBzdHJfZm9ybWF0LmZvcm1hdC5jYWxsKG51bGwsIHN0cl9mb3JtYXQuY2FjaGVbYXJndW1lbnRzWzBdXSwgYXJndW1lbnRzKTtcbiAgfTtcblxuICBzdHJfZm9ybWF0LmZvcm1hdCA9IGZ1bmN0aW9uKHBhcnNlX3RyZWUsIGFyZ3YpIHtcbiAgICB2YXIgY3Vyc29yID0gMSwgdHJlZV9sZW5ndGggPSBwYXJzZV90cmVlLmxlbmd0aCwgbm9kZV90eXBlID0gJycsIGFyZywgb3V0cHV0ID0gW10sIGksIGssIG1hdGNoLCBwYWQsIHBhZF9jaGFyYWN0ZXIsIHBhZF9sZW5ndGg7XG4gICAgZm9yIChpID0gMDsgaSA8IHRyZWVfbGVuZ3RoOyBpKyspIHtcbiAgICAgIG5vZGVfdHlwZSA9IGdldF90eXBlKHBhcnNlX3RyZWVbaV0pO1xuICAgICAgaWYgKG5vZGVfdHlwZSA9PT0gJ3N0cmluZycpIHtcbiAgICAgICAgb3V0cHV0LnB1c2gocGFyc2VfdHJlZVtpXSk7XG4gICAgICB9XG4gICAgICBlbHNlIGlmIChub2RlX3R5cGUgPT09ICdhcnJheScpIHtcbiAgICAgICAgbWF0Y2ggPSBwYXJzZV90cmVlW2ldOyAvLyBjb252ZW5pZW5jZSBwdXJwb3NlcyBvbmx5XG4gICAgICAgIGlmIChtYXRjaFsyXSkgeyAvLyBrZXl3b3JkIGFyZ3VtZW50XG4gICAgICAgICAgYXJnID0gYXJndltjdXJzb3JdO1xuICAgICAgICAgIGZvciAoayA9IDA7IGsgPCBtYXRjaFsyXS5sZW5ndGg7IGsrKykge1xuICAgICAgICAgICAgaWYgKCFhcmcuaGFzT3duUHJvcGVydHkobWF0Y2hbMl1ba10pKSB7XG4gICAgICAgICAgICAgIHRocm93IG5ldyBFcnJvcihzcHJpbnRmKCdbXy5zcHJpbnRmXSBwcm9wZXJ0eSBcIiVzXCIgZG9lcyBub3QgZXhpc3QnLCBtYXRjaFsyXVtrXSkpO1xuICAgICAgICAgICAgfVxuICAgICAgICAgICAgYXJnID0gYXJnW21hdGNoWzJdW2tdXTtcbiAgICAgICAgICB9XG4gICAgICAgIH0gZWxzZSBpZiAobWF0Y2hbMV0pIHsgLy8gcG9zaXRpb25hbCBhcmd1bWVudCAoZXhwbGljaXQpXG4gICAgICAgICAgYXJnID0gYXJndlttYXRjaFsxXV07XG4gICAgICAgIH1cbiAgICAgICAgZWxzZSB7IC8vIHBvc2l0aW9uYWwgYXJndW1lbnQgKGltcGxpY2l0KVxuICAgICAgICAgIGFyZyA9IGFyZ3ZbY3Vyc29yKytdO1xuICAgICAgICB9XG5cbiAgICAgICAgaWYgKC9bXnNdLy50ZXN0KG1hdGNoWzhdKSAmJiAoZ2V0X3R5cGUoYXJnKSAhPSAnbnVtYmVyJykpIHtcbiAgICAgICAgICB0aHJvdyBuZXcgRXJyb3Ioc3ByaW50ZignW18uc3ByaW50Zl0gZXhwZWN0aW5nIG51bWJlciBidXQgZm91bmQgJXMnLCBnZXRfdHlwZShhcmcpKSk7XG4gICAgICAgIH1cbiAgICAgICAgc3dpdGNoIChtYXRjaFs4XSkge1xuICAgICAgICAgIGNhc2UgJ2InOiBhcmcgPSBhcmcudG9TdHJpbmcoMik7IGJyZWFrO1xuICAgICAgICAgIGNhc2UgJ2MnOiBhcmcgPSBTdHJpbmcuZnJvbUNoYXJDb2RlKGFyZyk7IGJyZWFrO1xuICAgICAgICAgIGNhc2UgJ2QnOiBhcmcgPSBwYXJzZUludChhcmcsIDEwKTsgYnJlYWs7XG4gICAgICAgICAgY2FzZSAnZSc6IGFyZyA9IG1hdGNoWzddID8gYXJnLnRvRXhwb25lbnRpYWwobWF0Y2hbN10pIDogYXJnLnRvRXhwb25lbnRpYWwoKTsgYnJlYWs7XG4gICAgICAgICAgY2FzZSAnZic6IGFyZyA9IG1hdGNoWzddID8gcGFyc2VGbG9hdChhcmcpLnRvRml4ZWQobWF0Y2hbN10pIDogcGFyc2VGbG9hdChhcmcpOyBicmVhaztcbiAgICAgICAgICBjYXNlICdvJzogYXJnID0gYXJnLnRvU3RyaW5nKDgpOyBicmVhaztcbiAgICAgICAgICBjYXNlICdzJzogYXJnID0gKChhcmcgPSBTdHJpbmcoYXJnKSkgJiYgbWF0Y2hbN10gPyBhcmcuc3Vic3RyaW5nKDAsIG1hdGNoWzddKSA6IGFyZyk7IGJyZWFrO1xuICAgICAgICAgIGNhc2UgJ3UnOiBhcmcgPSBNYXRoLmFicyhhcmcpOyBicmVhaztcbiAgICAgICAgICBjYXNlICd4JzogYXJnID0gYXJnLnRvU3RyaW5nKDE2KTsgYnJlYWs7XG4gICAgICAgICAgY2FzZSAnWCc6IGFyZyA9IGFyZy50b1N0cmluZygxNikudG9VcHBlckNhc2UoKTsgYnJlYWs7XG4gICAgICAgIH1cbiAgICAgICAgYXJnID0gKC9bZGVmXS8udGVzdChtYXRjaFs4XSkgJiYgbWF0Y2hbM10gJiYgYXJnID49IDAgPyAnKycrIGFyZyA6IGFyZyk7XG4gICAgICAgIHBhZF9jaGFyYWN0ZXIgPSBtYXRjaFs0XSA/IG1hdGNoWzRdID09ICcwJyA/ICcwJyA6IG1hdGNoWzRdLmNoYXJBdCgxKSA6ICcgJztcbiAgICAgICAgcGFkX2xlbmd0aCA9IG1hdGNoWzZdIC0gU3RyaW5nKGFyZykubGVuZ3RoO1xuICAgICAgICBwYWQgPSBtYXRjaFs2XSA/IHN0cl9yZXBlYXQocGFkX2NoYXJhY3RlciwgcGFkX2xlbmd0aCkgOiAnJztcbiAgICAgICAgb3V0cHV0LnB1c2gobWF0Y2hbNV0gPyBhcmcgKyBwYWQgOiBwYWQgKyBhcmcpO1xuICAgICAgfVxuICAgIH1cbiAgICByZXR1cm4gb3V0cHV0LmpvaW4oJycpO1xuICB9O1xuXG4gIHN0cl9mb3JtYXQuY2FjaGUgPSB7fTtcblxuICBzdHJfZm9ybWF0LnBhcnNlID0gZnVuY3Rpb24oZm10KSB7XG4gICAgdmFyIF9mbXQgPSBmbXQsIG1hdGNoID0gW10sIHBhcnNlX3RyZWUgPSBbXSwgYXJnX25hbWVzID0gMDtcbiAgICB3aGlsZSAoX2ZtdCkge1xuICAgICAgaWYgKChtYXRjaCA9IC9eW15cXHgyNV0rLy5leGVjKF9mbXQpKSAhPT0gbnVsbCkge1xuICAgICAgICBwYXJzZV90cmVlLnB1c2gobWF0Y2hbMF0pO1xuICAgICAgfVxuICAgICAgZWxzZSBpZiAoKG1hdGNoID0gL15cXHgyNXsyfS8uZXhlYyhfZm10KSkgIT09IG51bGwpIHtcbiAgICAgICAgcGFyc2VfdHJlZS5wdXNoKCclJyk7XG4gICAgICB9XG4gICAgICBlbHNlIGlmICgobWF0Y2ggPSAvXlxceDI1KD86KFsxLTldXFxkKilcXCR8XFwoKFteXFwpXSspXFwpKT8oXFwrKT8oMHwnW14kXSk/KC0pPyhcXGQrKT8oPzpcXC4oXFxkKykpPyhbYi1mb3N1eFhdKS8uZXhlYyhfZm10KSkgIT09IG51bGwpIHtcbiAgICAgICAgaWYgKG1hdGNoWzJdKSB7XG4gICAgICAgICAgYXJnX25hbWVzIHw9IDE7XG4gICAgICAgICAgdmFyIGZpZWxkX2xpc3QgPSBbXSwgcmVwbGFjZW1lbnRfZmllbGQgPSBtYXRjaFsyXSwgZmllbGRfbWF0Y2ggPSBbXTtcbiAgICAgICAgICBpZiAoKGZpZWxkX21hdGNoID0gL14oW2Etel9dW2Etel9cXGRdKikvaS5leGVjKHJlcGxhY2VtZW50X2ZpZWxkKSkgIT09IG51bGwpIHtcbiAgICAgICAgICAgIGZpZWxkX2xpc3QucHVzaChmaWVsZF9tYXRjaFsxXSk7XG4gICAgICAgICAgICB3aGlsZSAoKHJlcGxhY2VtZW50X2ZpZWxkID0gcmVwbGFjZW1lbnRfZmllbGQuc3Vic3RyaW5nKGZpZWxkX21hdGNoWzBdLmxlbmd0aCkpICE9PSAnJykge1xuICAgICAgICAgICAgICBpZiAoKGZpZWxkX21hdGNoID0gL15cXC4oW2Etel9dW2Etel9cXGRdKikvaS5leGVjKHJlcGxhY2VtZW50X2ZpZWxkKSkgIT09IG51bGwpIHtcbiAgICAgICAgICAgICAgICBmaWVsZF9saXN0LnB1c2goZmllbGRfbWF0Y2hbMV0pO1xuICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgIGVsc2UgaWYgKChmaWVsZF9tYXRjaCA9IC9eXFxbKFxcZCspXFxdLy5leGVjKHJlcGxhY2VtZW50X2ZpZWxkKSkgIT09IG51bGwpIHtcbiAgICAgICAgICAgICAgICBmaWVsZF9saXN0LnB1c2goZmllbGRfbWF0Y2hbMV0pO1xuICAgICAgICAgICAgICB9XG4gICAgICAgICAgICAgIGVsc2Uge1xuICAgICAgICAgICAgICAgIHRocm93IG5ldyBFcnJvcignW18uc3ByaW50Zl0gaHVoPycpO1xuICAgICAgICAgICAgICB9XG4gICAgICAgICAgICB9XG4gICAgICAgICAgfVxuICAgICAgICAgIGVsc2Uge1xuICAgICAgICAgICAgdGhyb3cgbmV3IEVycm9yKCdbXy5zcHJpbnRmXSBodWg/Jyk7XG4gICAgICAgICAgfVxuICAgICAgICAgIG1hdGNoWzJdID0gZmllbGRfbGlzdDtcbiAgICAgICAgfVxuICAgICAgICBlbHNlIHtcbiAgICAgICAgICBhcmdfbmFtZXMgfD0gMjtcbiAgICAgICAgfVxuICAgICAgICBpZiAoYXJnX25hbWVzID09PSAzKSB7XG4gICAgICAgICAgdGhyb3cgbmV3IEVycm9yKCdbXy5zcHJpbnRmXSBtaXhpbmcgcG9zaXRpb25hbCBhbmQgbmFtZWQgcGxhY2Vob2xkZXJzIGlzIG5vdCAoeWV0KSBzdXBwb3J0ZWQnKTtcbiAgICAgICAgfVxuICAgICAgICBwYXJzZV90cmVlLnB1c2gobWF0Y2gpO1xuICAgICAgfVxuICAgICAgZWxzZSB7XG4gICAgICAgIHRocm93IG5ldyBFcnJvcignW18uc3ByaW50Zl0gaHVoPycpO1xuICAgICAgfVxuICAgICAgX2ZtdCA9IF9mbXQuc3Vic3RyaW5nKG1hdGNoWzBdLmxlbmd0aCk7XG4gICAgfVxuICAgIHJldHVybiBwYXJzZV90cmVlO1xuICB9O1xuXG4gIHJldHVybiBzdHJfZm9ybWF0O1xufSkoKTtcblxubW9kdWxlLmV4cG9ydHMgPSBzcHJpbnRmO1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG52YXIgdG9Qb3NpdGl2ZSA9IHJlcXVpcmUoJy4vaGVscGVyL3RvUG9zaXRpdmUnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBzdGFydHNXaXRoKHN0ciwgc3RhcnRzLCBwb3NpdGlvbikge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIHN0YXJ0cyA9ICcnICsgc3RhcnRzO1xuICBwb3NpdGlvbiA9IHBvc2l0aW9uID09IG51bGwgPyAwIDogTWF0aC5taW4odG9Qb3NpdGl2ZShwb3NpdGlvbiksIHN0ci5sZW5ndGgpO1xuICByZXR1cm4gc3RyLmxhc3RJbmRleE9mKHN0YXJ0cywgcG9zaXRpb24pID09PSBwb3NpdGlvbjtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBzdHJMZWZ0KHN0ciwgc2VwKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgc2VwID0gbWFrZVN0cmluZyhzZXApO1xuICB2YXIgcG9zID0gIXNlcCA/IC0xIDogc3RyLmluZGV4T2Yoc2VwKTtcbiAgcmV0dXJufiBwb3MgPyBzdHIuc2xpY2UoMCwgcG9zKSA6IHN0cjtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBzdHJMZWZ0QmFjayhzdHIsIHNlcCkge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIHNlcCA9IG1ha2VTdHJpbmcoc2VwKTtcbiAgdmFyIHBvcyA9IHN0ci5sYXN0SW5kZXhPZihzZXApO1xuICByZXR1cm5+IHBvcyA/IHN0ci5zbGljZSgwLCBwb3MpIDogc3RyO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHN0clJpZ2h0KHN0ciwgc2VwKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgc2VwID0gbWFrZVN0cmluZyhzZXApO1xuICB2YXIgcG9zID0gIXNlcCA/IC0xIDogc3RyLmluZGV4T2Yoc2VwKTtcbiAgcmV0dXJufiBwb3MgPyBzdHIuc2xpY2UocG9zICsgc2VwLmxlbmd0aCwgc3RyLmxlbmd0aCkgOiBzdHI7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc3RyUmlnaHRCYWNrKHN0ciwgc2VwKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgc2VwID0gbWFrZVN0cmluZyhzZXApO1xuICB2YXIgcG9zID0gIXNlcCA/IC0xIDogc3RyLmxhc3RJbmRleE9mKHNlcCk7XG4gIHJldHVybn4gcG9zID8gc3RyLnNsaWNlKHBvcyArIHNlcC5sZW5ndGgsIHN0ci5sZW5ndGgpIDogc3RyO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHN0cmlwVGFncyhzdHIpIHtcbiAgcmV0dXJuIG1ha2VTdHJpbmcoc3RyKS5yZXBsYWNlKC88XFwvP1tePl0rPi9nLCAnJyk7XG59O1xuIiwidmFyIGFkamFjZW50ID0gcmVxdWlyZSgnLi9oZWxwZXIvYWRqYWNlbnQnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiBzdWNjKHN0cikge1xuICByZXR1cm4gYWRqYWNlbnQoc3RyLCAxKTtcbn07XG4iLCJtb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHN1cnJvdW5kKHN0ciwgd3JhcHBlcikge1xuICByZXR1cm4gW3dyYXBwZXIsIHN0ciwgd3JhcHBlcl0uam9pbignJyk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gc3dhcENhc2Uoc3RyKSB7XG4gIHJldHVybiBtYWtlU3RyaW5nKHN0cikucmVwbGFjZSgvXFxTL2csIGZ1bmN0aW9uKGMpIHtcbiAgICByZXR1cm4gYyA9PT0gYy50b1VwcGVyQ2FzZSgpID8gYy50b0xvd2VyQ2FzZSgpIDogYy50b1VwcGVyQ2FzZSgpO1xuICB9KTtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB0aXRsZWl6ZShzdHIpIHtcbiAgcmV0dXJuIG1ha2VTdHJpbmcoc3RyKS50b0xvd2VyQ2FzZSgpLnJlcGxhY2UoLyg/Ol58XFxzfC0pXFxTL2csIGZ1bmN0aW9uKGMpIHtcbiAgICByZXR1cm4gYy50b1VwcGVyQ2FzZSgpO1xuICB9KTtcbn07XG4iLCJ2YXIgdHJpbSA9IHJlcXVpcmUoJy4vdHJpbScpO1xuXG5mdW5jdGlvbiBib29sTWF0Y2gocywgbWF0Y2hlcnMpIHtcbiAgdmFyIGksIG1hdGNoZXIsIGRvd24gPSBzLnRvTG93ZXJDYXNlKCk7XG4gIG1hdGNoZXJzID0gW10uY29uY2F0KG1hdGNoZXJzKTtcbiAgZm9yIChpID0gMDsgaSA8IG1hdGNoZXJzLmxlbmd0aDsgaSArPSAxKSB7XG4gICAgbWF0Y2hlciA9IG1hdGNoZXJzW2ldO1xuICAgIGlmICghbWF0Y2hlcikgY29udGludWU7XG4gICAgaWYgKG1hdGNoZXIudGVzdCAmJiBtYXRjaGVyLnRlc3QocykpIHJldHVybiB0cnVlO1xuICAgIGlmIChtYXRjaGVyLnRvTG93ZXJDYXNlKCkgPT09IGRvd24pIHJldHVybiB0cnVlO1xuICB9XG59XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdG9Cb29sZWFuKHN0ciwgdHJ1ZVZhbHVlcywgZmFsc2VWYWx1ZXMpIHtcbiAgaWYgKHR5cGVvZiBzdHIgPT09IFwibnVtYmVyXCIpIHN0ciA9IFwiXCIgKyBzdHI7XG4gIGlmICh0eXBlb2Ygc3RyICE9PSBcInN0cmluZ1wiKSByZXR1cm4gISFzdHI7XG4gIHN0ciA9IHRyaW0oc3RyKTtcbiAgaWYgKGJvb2xNYXRjaChzdHIsIHRydWVWYWx1ZXMgfHwgW1widHJ1ZVwiLCBcIjFcIl0pKSByZXR1cm4gdHJ1ZTtcbiAgaWYgKGJvb2xNYXRjaChzdHIsIGZhbHNlVmFsdWVzIHx8IFtcImZhbHNlXCIsIFwiMFwiXSkpIHJldHVybiBmYWxzZTtcbn07XG4iLCJ2YXIgdHJpbSA9IHJlcXVpcmUoJy4vdHJpbScpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHRvTnVtYmVyKG51bSwgcHJlY2lzaW9uKSB7XG4gIGlmIChudW0gPT0gbnVsbCkgcmV0dXJuIDA7XG4gIHZhciBmYWN0b3IgPSBNYXRoLnBvdygxMCwgaXNGaW5pdGUocHJlY2lzaW9uKSA/IHByZWNpc2lvbiA6IDApO1xuICByZXR1cm4gTWF0aC5yb3VuZChudW0gKiBmYWN0b3IpIC8gZmFjdG9yO1xufTtcbiIsInZhciBydHJpbSA9IHJlcXVpcmUoJy4vcnRyaW0nKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB0b1NlbnRlbmNlKGFycmF5LCBzZXBhcmF0b3IsIGxhc3RTZXBhcmF0b3IsIHNlcmlhbCkge1xuICBzZXBhcmF0b3IgPSBzZXBhcmF0b3IgfHwgJywgJztcbiAgbGFzdFNlcGFyYXRvciA9IGxhc3RTZXBhcmF0b3IgfHwgJyBhbmQgJztcbiAgdmFyIGEgPSBhcnJheS5zbGljZSgpLFxuICAgIGxhc3RNZW1iZXIgPSBhLnBvcCgpO1xuXG4gIGlmIChhcnJheS5sZW5ndGggPiAyICYmIHNlcmlhbCkgbGFzdFNlcGFyYXRvciA9IHJ0cmltKHNlcGFyYXRvcikgKyBsYXN0U2VwYXJhdG9yO1xuXG4gIHJldHVybiBhLmxlbmd0aCA/IGEuam9pbihzZXBhcmF0b3IpICsgbGFzdFNlcGFyYXRvciArIGxhc3RNZW1iZXIgOiBsYXN0TWVtYmVyO1xufTtcbiIsInZhciB0b1NlbnRlbmNlID0gcmVxdWlyZSgnLi90b1NlbnRlbmNlJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdG9TZW50ZW5jZVNlcmlhbChhcnJheSwgc2VwLCBsYXN0U2VwKSB7XG4gIHJldHVybiB0b1NlbnRlbmNlKGFycmF5LCBzZXAsIGxhc3RTZXAsIHRydWUpO1xufTtcbiIsInZhciBtYWtlU3RyaW5nID0gcmVxdWlyZSgnLi9oZWxwZXIvbWFrZVN0cmluZycpO1xudmFyIGRlZmF1bHRUb1doaXRlU3BhY2UgPSByZXF1aXJlKCcuL2hlbHBlci9kZWZhdWx0VG9XaGl0ZVNwYWNlJyk7XG52YXIgbmF0aXZlVHJpbSA9IFN0cmluZy5wcm90b3R5cGUudHJpbTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB0cmltKHN0ciwgY2hhcmFjdGVycykge1xuICBzdHIgPSBtYWtlU3RyaW5nKHN0cik7XG4gIGlmICghY2hhcmFjdGVycyAmJiBuYXRpdmVUcmltKSByZXR1cm4gbmF0aXZlVHJpbS5jYWxsKHN0cik7XG4gIGNoYXJhY3RlcnMgPSBkZWZhdWx0VG9XaGl0ZVNwYWNlKGNoYXJhY3RlcnMpO1xuICByZXR1cm4gc3RyLnJlcGxhY2UobmV3IFJlZ0V4cCgnXicgKyBjaGFyYWN0ZXJzICsgJyt8JyArIGNoYXJhY3RlcnMgKyAnKyQnLCAnZycpLCAnJyk7XG59O1xuIiwidmFyIG1ha2VTdHJpbmcgPSByZXF1aXJlKCcuL2hlbHBlci9tYWtlU3RyaW5nJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdHJ1bmNhdGUoc3RyLCBsZW5ndGgsIHRydW5jYXRlU3RyKSB7XG4gIHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcbiAgdHJ1bmNhdGVTdHIgPSB0cnVuY2F0ZVN0ciB8fCAnLi4uJztcbiAgbGVuZ3RoID0gfn5sZW5ndGg7XG4gIHJldHVybiBzdHIubGVuZ3RoID4gbGVuZ3RoID8gc3RyLnNsaWNlKDAsIGxlbmd0aCkgKyB0cnVuY2F0ZVN0ciA6IHN0cjtcbn07XG4iLCJ2YXIgdHJpbSA9IHJlcXVpcmUoJy4vdHJpbScpO1xuXG5tb2R1bGUuZXhwb3J0cyA9IGZ1bmN0aW9uIHVuZGVyc2NvcmVkKHN0cikge1xuICByZXR1cm4gdHJpbShzdHIpLnJlcGxhY2UoLyhbYS16XFxkXSkoW0EtWl0rKS9nLCAnJDFfJDInKS5yZXBsYWNlKC9bLVxcc10rL2csICdfJykudG9Mb3dlckNhc2UoKTtcbn07XG4iLCJ2YXIgbWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcbnZhciBodG1sRW50aXRpZXMgPSByZXF1aXJlKCcuL2hlbHBlci9odG1sRW50aXRpZXMnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB1bmVzY2FwZUhUTUwoc3RyKSB7XG4gIHJldHVybiBtYWtlU3RyaW5nKHN0cikucmVwbGFjZSgvXFwmKFteO10rKTsvZywgZnVuY3Rpb24oZW50aXR5LCBlbnRpdHlDb2RlKSB7XG4gICAgdmFyIG1hdGNoO1xuXG4gICAgaWYgKGVudGl0eUNvZGUgaW4gaHRtbEVudGl0aWVzKSB7XG4gICAgICByZXR1cm4gaHRtbEVudGl0aWVzW2VudGl0eUNvZGVdO1xuICAgIH0gZWxzZSBpZiAobWF0Y2ggPSBlbnRpdHlDb2RlLm1hdGNoKC9eI3goW1xcZGEtZkEtRl0rKSQvKSkge1xuICAgICAgcmV0dXJuIFN0cmluZy5mcm9tQ2hhckNvZGUocGFyc2VJbnQobWF0Y2hbMV0sIDE2KSk7XG4gICAgfSBlbHNlIGlmIChtYXRjaCA9IGVudGl0eUNvZGUubWF0Y2goL14jKFxcZCspJC8pKSB7XG4gICAgICByZXR1cm4gU3RyaW5nLmZyb21DaGFyQ29kZSh+fm1hdGNoWzFdKTtcbiAgICB9IGVsc2Uge1xuICAgICAgcmV0dXJuIGVudGl0eTtcbiAgICB9XG4gIH0pO1xufTtcbiIsIm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdW5xdW90ZShzdHIsIHF1b3RlQ2hhcikge1xuICBxdW90ZUNoYXIgPSBxdW90ZUNoYXIgfHwgJ1wiJztcbiAgaWYgKHN0clswXSA9PT0gcXVvdGVDaGFyICYmIHN0cltzdHIubGVuZ3RoIC0gMV0gPT09IHF1b3RlQ2hhcilcbiAgICByZXR1cm4gc3RyLnNsaWNlKDEsIHN0ci5sZW5ndGggLSAxKTtcbiAgZWxzZSByZXR1cm4gc3RyO1xufTtcbiIsInZhciBzcHJpbnRmID0gcmVxdWlyZSgnLi9zcHJpbnRmJyk7XG5cbm1vZHVsZS5leHBvcnRzID0gZnVuY3Rpb24gdnNwcmludGYoZm10LCBhcmd2KSB7XG4gIGFyZ3YudW5zaGlmdChmbXQpO1xuICByZXR1cm4gc3ByaW50Zi5hcHBseShudWxsLCBhcmd2KTtcbn07XG4iLCJ2YXIgaXNCbGFuayA9IHJlcXVpcmUoJy4vaXNCbGFuaycpO1xudmFyIHRyaW0gPSByZXF1aXJlKCcuL3RyaW0nKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB3b3JkcyhzdHIsIGRlbGltaXRlcikge1xuICBpZiAoaXNCbGFuayhzdHIpKSByZXR1cm4gW107XG4gIHJldHVybiB0cmltKHN0ciwgZGVsaW1pdGVyKS5zcGxpdChkZWxpbWl0ZXIgfHwgL1xccysvKTtcbn07XG4iLCIvLyBXcmFwXG4vLyB3cmFwcyBhIHN0cmluZyBieSBhIGNlcnRhaW4gd2lkdGhcblxubWFrZVN0cmluZyA9IHJlcXVpcmUoJy4vaGVscGVyL21ha2VTdHJpbmcnKTtcblxubW9kdWxlLmV4cG9ydHMgPSBmdW5jdGlvbiB3cmFwKHN0ciwgb3B0aW9ucyl7XG5cdHN0ciA9IG1ha2VTdHJpbmcoc3RyKTtcblxuXHRvcHRpb25zID0gb3B0aW9ucyB8fCB7fTtcblxuXHR3aWR0aCA9IG9wdGlvbnMud2lkdGggfHwgNzU7XG5cdHNlcGVyYXRvciA9IG9wdGlvbnMuc2VwZXJhdG9yIHx8ICdcXG4nO1xuXHRjdXQgPSBvcHRpb25zLmN1dCB8fCBmYWxzZTtcblx0cHJlc2VydmVTcGFjZXMgPSBvcHRpb25zLnByZXNlcnZlU3BhY2VzIHx8IGZhbHNlO1xuXHR0cmFpbGluZ1NwYWNlcyA9IG9wdGlvbnMudHJhaWxpbmdTcGFjZXMgfHwgZmFsc2U7XG5cblx0aWYod2lkdGggPD0gMCl7XG5cdFx0cmV0dXJuIHN0cjtcblx0fVxuXG5cdGVsc2UgaWYoIWN1dCl7XG5cblx0XHR3b3JkcyA9IHN0ci5zcGxpdChcIiBcIik7XG5cdFx0cmVzdWx0ID0gXCJcIjtcblx0XHRjdXJyZW50X2NvbHVtbiA9IDA7XG5cblx0XHR3aGlsZSh3b3Jkcy5sZW5ndGggPiAwKXtcblx0XHRcdFxuXHRcdFx0Ly8gaWYgYWRkaW5nIGEgc3BhY2UgYW5kIHRoZSBuZXh0IHdvcmQgd291bGQgY2F1c2UgdGhpcyBsaW5lIHRvIGJlIGxvbmdlciB0aGFuIHdpZHRoLi4uXG5cdFx0XHRpZigxICsgd29yZHNbMF0ubGVuZ3RoICsgY3VycmVudF9jb2x1bW4gPiB3aWR0aCl7XG5cdFx0XHRcdC8vc3RhcnQgYSBuZXcgbGluZSBpZiB0aGlzIGxpbmUgaXMgbm90IGFscmVhZHkgZW1wdHlcblx0XHRcdFx0aWYoY3VycmVudF9jb2x1bW4gPiAwKXtcblx0XHRcdFx0XHQvLyBhZGQgYSBzcGFjZSBhdCB0aGUgZW5kIG9mIHRoZSBsaW5lIGlzIHByZXNlcnZlU3BhY2VzIGlzIHRydWVcblx0XHRcdFx0XHRpZiAocHJlc2VydmVTcGFjZXMpe1xuXHRcdFx0XHRcdFx0cmVzdWx0ICs9ICcgJztcblx0XHRcdFx0XHRcdGN1cnJlbnRfY29sdW1uKys7XG5cdFx0XHRcdFx0fVxuXHRcdFx0XHRcdC8vIGZpbGwgdGhlIHJlc3Qgb2YgdGhlIGxpbmUgd2l0aCBzcGFjZXMgaWYgdHJhaWxpbmdTcGFjZXMgb3B0aW9uIGlzIHRydWVcblx0XHRcdFx0XHRlbHNlIGlmKHRyYWlsaW5nU3BhY2VzKXtcblx0XHRcdFx0XHRcdHdoaWxlKGN1cnJlbnRfY29sdW1uIDwgd2lkdGgpe1xuXHRcdFx0XHRcdFx0XHRyZXN1bHQgKz0gJyAnO1xuXHRcdFx0XHRcdFx0XHRjdXJyZW50X2NvbHVtbisrO1xuXHRcdFx0XHRcdFx0fVx0XHRcdFx0XHRcdFxuXHRcdFx0XHRcdH1cblx0XHRcdFx0XHQvL3N0YXJ0IG5ldyBsaW5lXG5cdFx0XHRcdFx0cmVzdWx0ICs9IHNlcGVyYXRvcjtcblx0XHRcdFx0XHRjdXJyZW50X2NvbHVtbiA9IDA7XG5cdFx0XHRcdH1cblx0XHRcdH1cblxuXHRcdFx0Ly8gaWYgbm90IGF0IHRoZSBiZWdpbmluZyBvZiB0aGUgbGluZSwgYWRkIGEgc3BhY2UgaW4gZnJvbnQgb2YgdGhlIHdvcmRcblx0XHRcdGlmKGN1cnJlbnRfY29sdW1uID4gMCl7XG5cdFx0XHRcdHJlc3VsdCArPSBcIiBcIjtcblx0XHRcdFx0Y3VycmVudF9jb2x1bW4rKztcblx0XHRcdH1cblxuXHRcdFx0Ly8gdGFjayBvbiB0aGUgbmV4dCB3b3JkLCB1cGRhdGUgY3VycmVudCBjb2x1bW4sIGEgcG9wIHdvcmRzIGFycmF5XG5cdFx0XHRyZXN1bHQgKz0gd29yZHNbMF07XG5cdFx0XHRjdXJyZW50X2NvbHVtbiArPSB3b3Jkc1swXS5sZW5ndGg7XG5cdFx0XHR3b3Jkcy5zaGlmdCgpO1xuXG5cdFx0fVxuXG5cdFx0Ly8gZmlsbCB0aGUgcmVzdCBvZiB0aGUgbGluZSB3aXRoIHNwYWNlcyBpZiB0cmFpbGluZ1NwYWNlcyBvcHRpb24gaXMgdHJ1ZVxuXHRcdGlmKHRyYWlsaW5nU3BhY2VzKXtcblx0XHRcdHdoaWxlKGN1cnJlbnRfY29sdW1uIDwgd2lkdGgpe1xuXHRcdFx0XHRyZXN1bHQgKz0gJyAnO1xuXHRcdFx0XHRjdXJyZW50X2NvbHVtbisrO1xuXHRcdFx0fVx0XHRcdFx0XHRcdFxuXHRcdH1cblxuXHRcdHJldHVybiByZXN1bHQ7XG5cblx0fVxuXG5cdGVsc2Uge1xuXG5cdFx0aW5kZXggPSAwO1xuXHRcdHJlc3VsdCA9IFwiXCI7XG5cblx0XHQvLyB3YWxrIHRocm91Z2ggZWFjaCBjaGFyYWN0ZXIgYW5kIGFkZCBzZXBlcmF0b3JzIHdoZXJlIGFwcHJvcHJpYXRlXG5cdFx0d2hpbGUoaW5kZXggPCBzdHIubGVuZ3RoKXtcblx0XHRcdGlmKGluZGV4ICUgd2lkdGggPT0gMCAmJiBpbmRleCA+IDApe1xuXHRcdFx0XHRyZXN1bHQgKz0gc2VwZXJhdG9yO1xuXHRcdFx0fVxuXHRcdFx0cmVzdWx0ICs9IHN0ci5jaGFyQXQoaW5kZXgpO1xuXHRcdFx0aW5kZXgrKztcblx0XHR9XG5cblx0XHQvLyBmaWxsIHRoZSByZXN0IG9mIHRoZSBsaW5lIHdpdGggc3BhY2VzIGlmIHRyYWlsaW5nU3BhY2VzIG9wdGlvbiBpcyB0cnVlXG5cdFx0aWYodHJhaWxpbmdTcGFjZXMpe1xuXHRcdFx0d2hpbGUoaW5kZXggJSB3aWR0aCA+IDApe1xuXHRcdFx0XHRyZXN1bHQgKz0gJyAnO1xuXHRcdFx0XHRpbmRleCsrO1xuXHRcdFx0fVx0XHRcdFx0XHRcdFxuXHRcdH1cblx0XHRcblx0XHRyZXR1cm4gcmVzdWx0O1xuXHR9XG59OyJdfQ==
