var verify = require('http-signature')

module.exports = function (callback, myKeyId, mySignature, mySigningString, myPubKey) {
    var parsedSignature = {
        scheme: 'Signature',
        algorithm: 'rsa-sha256',
        params: {
            keyId: myKeyId,
            headers: ['digest', 'date'],
            signature: mySignature
        },
        signingString: mySigningString
    }

    callback(null, verify.verifySignature(parsedSignature, myPubKey));
}