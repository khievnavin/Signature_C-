using System.Security.Cryptography;
using System.Text;

public class SignatureService
{
    private RSA _rsa;

    public SignatureService()
    {
        _rsa = RSA.Create(2048); // Generate 2048-bit key pair
    }

    public (string publicKey, string privateKey) GetKeys()
    {
        var publicKey = Convert.ToBase64String(_rsa.ExportSubjectPublicKeyInfo());
        var privateKey = Convert.ToBase64String(_rsa.ExportPkcs8PrivateKey());
        return (publicKey, privateKey);
    }

    public string SignData(string data)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        var signature = _rsa.SignData(dataBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        return Convert.ToBase64String(signature);
    }

    public bool VerifySignature(string data, string signatureBase64)
    {
        var dataBytes = Encoding.UTF8.GetBytes(data);
        var signatureBytes = Convert.FromBase64String(signatureBase64);
        return _rsa.VerifyData(dataBytes, signatureBytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
    }
}
