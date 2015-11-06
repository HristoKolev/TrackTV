'use strict';

var expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

var pathChain = require('../modules/pathChain');

describe('#pathChain', function () {

    describe('module exports', function () {

        assertCompositionMultitest.object('pathChain', pathChain, [
            ['instance', 'function']
        ]);
    });

    function assertIsChain(chain) {

        assertComposition.function(chain);

        assertComposition.object('chain', chain, [
            ['value', 'function']
        ]);
    }

    describe('#instance()', function () {

        it('should create new node', function () {

            var chain = pathChain.instance('path');

            assertIsChain(chain);
        });
    });

    it('should return new node when called with sting path', function () {

        var instance = pathChain.instance('/test');

        var node = instance('dir');

        assertIsChain(node);
    });

    it('should return the the correct value', function () {

        var path = 'dir';

        var node = pathChain.instance(path);

        expect(node.value()).to.be.equal(path);
    });

    it('should expand the value of the node that created it', function () {

        var parentPath = 'dir';

        var parentNode = pathChain.instance(parentPath);

        var nodePath = 'file';

        var node = parentNode(nodePath);

        expect(node.value()).to.be.equal('dir\\file');
    });

    it('should throw if the base path is a falsy value', function () {

        expect(function () {

            pathChain.instance(null);

        }).to.throw(/path is invalid/);
    });
});