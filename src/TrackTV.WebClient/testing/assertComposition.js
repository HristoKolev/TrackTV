'use strict';

var expect = require('chai').expect;

function formatName(name, type) {

    var result = '#' + name;

    if (type === 'function') {

        result += '()';
    }

    return result;
}

function getMemberInfo(member) {

    if (Array.isArray(member) && member.length === 1) {

        member = member[0];
    }

    var hasType = Array.isArray(member) && member.length > 1;

    var memberName;
    var memberType;

    if (hasType) {

        memberName = member[0];
        memberType = member[1];

    } else {

        memberName = member;
    }

    return {
        hasType: hasType,
        name: memberName,
        type: memberType,
        formattedName: formatName(memberName, memberType)
    };
}

function getTitle(objectName, member) {

    var memberInfo = getMemberInfo(member);

    var message = '#' + objectName + ' should have property [' + (memberInfo.type || 'any') + '] ' + memberInfo.formattedName;

    return message;
}

function testMember(objectName, obj, member, multitest) {

    var memberInfo = getMemberInfo(member);

    var title = getTitle(objectName, member);

    function executeTest() {

        var member = obj[memberInfo.name];

        expect(member).to.exist;

        if (memberInfo.hasType) {

            expect(member).to.be.a(memberInfo.type);
        }
    }

    if (multitest) {

        it(title, executeTest);

    } else {

        executeTest();
    }
}

function assertComposition(multitest) {

    var that = Object.create(null);

    that.object = function (objectName, obj, members) {

        for (var i = 0; i < members.length; i += 1) {

            var member = members[i];

            testMember(objectName, obj, member, multitest);
        }
    };

    that.function = function (name, obj) {

        function executeTest() {

            expect(obj).to.be.a('function');
        }

        if (multitest) {

            it('#' + name + '() should be a function', executeTest);

        } else {

            executeTest();
        }
    };

    return that;
}

module.exports = assertComposition(false);
module.exports.multitest = assertComposition(true);