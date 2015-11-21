'use strict';

let expect = require('chai').expect,
    assertCompositionMultitest = require('../testing/assertComposition').multitest,
    assertComposition = require('../testing/assertComposition');

let pathChain = require('../modules/pathChain');

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

            let chain = pathChain.instance('path');

            assertIsChain(chain);
        });
    });

    it('should return new node when called with sting path', function () {

        let instance = pathChain.instance('/test');

        let node = instance('dir');

        assertIsChain(node);
    });

    it('should return the the correct value', function () {

        let path = 'dir';

        let node = pathChain.instance(path);

        expect(node.value()).to.be.equal(path);
    });

    it('should expand the value of the node that created it', function () {

        let parentPath = 'dir';

        let parentNode = pathChain.instance(parentPath);

        let nodePath = 'file';

        let node = parentNode(nodePath);

        expect(node.value()).to.be.equal('dir\\file');
    });

    it('should throw if the base path is a falsy value', function () {

        expect(function () {

            pathChain.instance(null);

        }).to.throw(/path is invalid/);
    });
});