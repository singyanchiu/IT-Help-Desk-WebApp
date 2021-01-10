using System.Diagnostics;
using DataLibraryNew2.DataAccess;
using DataLibraryNew2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLibraryNew2.BusinessLogic
{
    public class RequestProcessor
    {
        public static int CreateRequest(string subject, string description, string userId, int statusId)
        {
            Request data = new Request
            {
                Subject = subject,
                Description = description
            };

            //Query data and get the id for the current entry
            string sql = @"INSERT INTO dbo.Request (Subject, Description) VALUES (@Subject, @Description);
                           SELECT CAST(SCOPE_IDENTITY() as int)";
            int requestId = SqlDataAccess.SaveDataGetId(sql, data);

            //Update RequestUser table - assigning user to this request
            RequestUser data2 = new RequestUser
            {
                RequestId = requestId,
                UserId = userId
            };
            string sql2 = @"INSERT INTO dbo.RequestUser (RequestId, UserId)
                          values (@RequestId, @UserId);";
            SqlDataAccess.SaveData(sql2, data2);

            //Update RequestStatus table - assigning status to this request
            RequestStatus data3 = new RequestStatus
            {
                RequestId = requestId,
                StatusId = statusId
            };
            string sql3 = @"INSERT INTO dbo.RequestStatus (RequestId, StatusId)
                          values (@RequestId, @StatusId);";
            SqlDataAccess.SaveData(sql3, data3);
            
            return 1; 
        }

        public static int EditRequest(string subject, string description, string userId, int statusId, int requestId)
        {
            Request data = new Request
            {
                Subject = subject,
                Description = description
            };
            string sql = @"UPDATE dbo.Request
                            SET Subject = @Subject,
                                Description = @Description
                            WHERE Id = " + requestId + ";";
            SqlDataAccess.SaveData(sql, data);

            //Update RequestStatus table
            RequestStatus data2 = new RequestStatus
            {
                RequestId = requestId,
                StatusId = statusId
            };
            string sql2 = @"UPDATE dbo.RequestStatus
                            SET StatusId = @StatusId
                            WHERE RequestId =" + requestId + ";";
            SqlDataAccess.SaveData(sql2, data2);

            return 1;
        }

        public static List<Request> LoadRequests(string userId)
        {
            string sql;
            List<Request> requests;

            if (userId!=null)
            {
                sql = @"SELECT Id, Subject, Description FROM
                            dbo.Request WHERE Id IN (
                                SELECT RequestId 
                                FROM dbo.RequestUser 
                                where UserId = '" + userId + "');";

                //get all requests with this userId
                requests = SqlDataAccess.LoadData<Request>(sql);
            }
            else
            {
                sql = @"SELECT Id, Subject, Description
                                FROM dbo.Request;";
                //get all requests
                requests = SqlDataAccess.LoadData<Request>(sql);

                //populate the submitter for each request in the list
                foreach (var request in requests)
                {
                    string submitterId = GetUserIdByRequestId(request.Id);
                    request.Submitter = UserProcessor.GetUserByUserId(submitterId);
                }
            }

            //populate the statuses for each request in the list
            foreach (var request in requests)
            {
                int statusId = GetStatusIdByRequestId(request.Id);
                request.Status = GetStatusByStatusId(statusId);
            }

            return requests;
        }

        public static Request LoadRequest(int requestId)
        {
            string sql = @"SELECT Id, Subject, Description FROM
                            dbo.Request WHERE Id =" + requestId + ";";

            Request request = SqlDataAccess.LoadDataSingle<Request>(sql);

            //Retrieve the status of the request and add to it
            int statusId = GetStatusIdByRequestId(requestId);
            request.Status = GetStatusByStatusId(statusId);
            request.SelectedStatus = statusId;
            return request;
        }

        public static List<Status> GetStatusCollection()
        {
            string sql = @"SELECT Id, Name
                            FROM dbo.Status;";

            return SqlDataAccess.LoadData<Status>(sql);
        }

        public static int GetStatusIdByRequestId(int requestId)
        {
            string sql = @"SELECT StatusId
                           FROM dbo.RequestStatus
                           WHERE RequestId = "+requestId+";";

            string connectionName = "DefaultConnection";

            return SqlDataAccess.Query<int>(sql, connectionName);
        }

        public static string GetStatusByStatusId(int statusId)
        {
            string sql = @"SELECT Name
                           FROM dbo.Status
                           WHERE Id = " + statusId + ";";

            string connectionName = "DefaultConnection";

            return SqlDataAccess.Query<string>(sql, connectionName);
        }

        public static string GetUserIdByRequestId(int requestId)
        {
            string sql = @"SELECT UserId
                           FROM dbo.RequestUser
                           WHERE RequestId = " + requestId + ";";

            string connectionName = "DefaultConnection";

            return SqlDataAccess.Query<string>(sql, connectionName);
        }
    }
}
