using DataLibraryNew2.DataAccess;
using DataLibraryNew2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibraryNew2.BusinessLogic
{
    public static class ProjectProcessor
    {
        public static int CreateProject(string name, string description)
        {
            Project data = new Project
            {
                ProjectName = name,
                Description = description
            };

            string sql = @"INSERT INTO dbo.ProjectTable (ProjectName, Description)
                            values (@ProjectName, @Description);";

            return SqlDataAccess.SaveData(sql, data);
        }

        public static List<Project> LoadProjects()
        {
            string sql = @"SELECT Id, ProjectName, Description FROM
                            dbo.ProjectTable;";
            return SqlDataAccess.LoadData<Project>(sql);
        }
    }
}
