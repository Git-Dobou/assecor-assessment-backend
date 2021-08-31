using System.Collections.Generic;

namespace assecor_assessment_backend.Services.Color
{
    public interface IColorService
    {
        string GetColorNameById(int id);
        int? GetColorIdByName(string name);
    }
}