namespace assecor_assessment_backend.Services.Path
{
    public interface IPathService
    {
        string Combine(string path1, string path2);
        string GetSolutionDir();
    }
}