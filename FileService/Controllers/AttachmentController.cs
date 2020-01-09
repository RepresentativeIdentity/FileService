using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Threading.Tasks;
using Dapper;
using FileService.Models;
using FileService.SqlScripts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace FileService.Controllers
{
    [Authorize]
    [Produces("application/json")]
    [Route("api/Attachment")]
    public class AttachmentController : Controller
    {
        private readonly IConfiguration configuration;

        public AttachmentController(IConfiguration config)
        {
            configuration = config;
        }

        ScriptReader reader = new ScriptReader();


        [HttpGet("Document/{DProtocolID}")]
        public async Task<IActionResult> GetDocument(int dProtocolID)
        {
            // throw new Exception();
            string connectionString = configuration.GetConnectionString("DocSysBeta2");
            string sqlGetData = reader.Read("SqlScripts/RpsBinData/GetData.sql");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var permissionError = await CheckPermission(connection, dProtocolID);

                if (!string.IsNullOrEmpty(permissionError))
                    return RedirectToAction("SendError", "Errors", new { ErrorMessage = permissionError });

                var data = await connection.QueryFirstOrDefaultAsync<RpsBinDataDto>(sqlGetData, new { dProtocolID });

                return GetFile(data);
            }

        }



        [HttpGet("{BinDataID}")]
        public async Task<IActionResult> GetAttachment(int BinDataID)
        {
            // throw new Exception();
            string connectionString = configuration.GetConnectionString("DocSysBeta2");
            string sqlGetData = reader.Read("SqlScripts/RpsBinData/GetDataIDAttachment.sql");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                var permissionError = await CheckPermission(connection, BinDataID);

                if (!string.IsNullOrEmpty(permissionError))
                    return RedirectToAction("SendError", "Errors", new { ErrorMessage = permissionError });

                var data = await connection.QueryFirstOrDefaultAsync<RpsBinDataDto>(sqlGetData, new { BinDataID });

                return GetFile(data);
            }
        }



        [HttpGet("Document/All/{DProtocolID}")]
        public async Task<IActionResult> Links(int dProtocolID)
        {
            // throw new Exception();
            IEnumerable<RpsBinDataAttachmentListDto> data = null;

            string connectionString = configuration.GetConnectionString("DocSysBeta2");
            string sqlGetData = reader.Read("SqlScripts/RpsBinData/GetAllDocuments.sql");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                var permissionError = await CheckPermission(connection, dProtocolID);

                if (!string.IsNullOrEmpty(permissionError))
                    return RedirectToAction("SendError", "Errors", new { ErrorMessage = permissionError });

                data = await connection.QueryAsync<RpsBinDataAttachmentListDto>(sqlGetData, new { dProtocolID });

                if (data.Count() < 1)
                    return RedirectToAction("SendError", "Errors", new { ErrorMessage = "Ne postoje prilozi." });

                return View(data);
            }

        }



        private async Task<string> CheckPermission(SqlConnection connection, int dProtocolID)
        {
            var isLocal = configuration.GetSection("Logging:Local")["IsDevelopment"]; // potrebno promijeniti IsDevelopment na false za produkciju
            var loginName = User.Identity.Name;

            if (isLocal == "True")
                loginName = "puh";
            else
                loginName = loginName.Split("\\").Last();


            string sqlGetSID = reader.Read("SqlScripts/DSUser/GetSID.sql");
            string sqlGetDocumentTypeID = reader.Read("SqlScripts/DProtocol/GetDocumentTypeID.sql");
            string sqlCheckPermission = reader.Read("SqlScripts/FilePermission/CheckFilePermission.sql");


            var userSID = await connection.QueryFirstOrDefaultAsync<int?>(sqlGetSID, new { loginName });

            if (userSID == null)
                return "Korisnik nije pronađen.";

            var documentTypeID = await connection.QueryFirstOrDefaultAsync<int>(sqlGetDocumentTypeID, new { dProtocolID });

            var permission = await connection.QueryFirstOrDefaultAsync<bool>(sqlCheckPermission, new { documentTypeID, userSID });
            
            if (!permission)
                return "Nemate prava za pristup podacima.";

            return string.Empty;

        }



        private IActionResult GetFile(RpsBinDataDto data)
        {
            if (data == null)
                return RedirectToAction("SendError", "Errors", new { ErrorMessage = "Ne postoje prilozi." });

            var fName = data.FileName;

            Response.Headers.Append("Content-Disposition", "attachment; filename=" + fName);
            return File(data.Data, MimeTypes.GetMimeType(fName));
        }

    }

}
