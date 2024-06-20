namespace Application.Interfaces;

public interface IEncryptionService
{
    string Hash(string input);
    bool Verify(string candidateInput, string hash);
}
