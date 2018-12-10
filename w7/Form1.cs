// fully automated
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OracleClient;
using System.Xml;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Xml.Schema;
using System.IO;
using System.Threading;

namespace w7
{
    public partial class Form1 : Form
    {
        public OracleConnection conn;
        public OracleCommand cmd,cmd1,cmd2,cmd3,cmd5,cmd6;
        public string lstday, lstmonth, lstyear;
        public int zippedalready = 0;
        public int dateselected = 0;
        public int dateselectedn = 0;
        public int maxappno;
        public int minappno;
       // public Boolean retry = false;

       
        public int fullordiff = 0;
        string ftpServerIP;
        string ftpRemotePath;
        string ftpUserID;
        string ftpPassword;
        string ftpURI;
        public static string validfile;
        public static string filenameonly;
       
        public string filetosend1, filetosend2;
        public string foldertosend1, foldertosend2;
        public string s1, files, filesi;
        public string grtl, grtli;
        public int folnomax = 0;
        public DateTime stdate;
        public string datestr, rootf, datef;
        //public DateTime stdate = DateTime.Now;
        //string datestr = stdate.Year.ToString() + "-" + stdate.Month.ToString() + "-" + stdate.Day.ToString();
        //string rootf = "D:/";
        //string datef = datestr + "/";
        public int cnt = 0; // file name counter 
        public string[] filenames = new string[100000];  // string array : that use to store names of files 
        public string[] foldernamesxml = new string[1000];  // string array : that use to store names of files 
        public string[] foldernamesimg = new string[1000];  // string array : that use to store names of files 
      
       
      

        void deletedir(string pth)
        { // this modile is to make directory empty 
            System.IO.DirectoryInfo di = new DirectoryInfo(pth);
            foreach (FileInfo file in di.GetFiles())
            {
                file.Delete();
            }
            foreach (DirectoryInfo dir in di.GetDirectories())
            {
                dir.Delete(true);
            }
        }


        public string[] filesindir(string pth)
        { // this modile is to make directory empty 
            string[] s = new string[10];
            int lvar = 0;
            System.IO.DirectoryInfo di = new DirectoryInfo(pth);
            foreach (FileInfo file in di.GetFiles())
            {
                s[lvar] = file.Name;
                lvar = lvar + 1;
             }
            return s;
        }
       

 

 public void Upload(string filestoupload, string ftpRemotePath,string xmlorimage)
        {
            string ftpServerIP;
            //string ftpRemotePath;
            string ftpUserID;
            string ftpPassword;
            string ftpURI;
            //--FTP upload section start
           // ftpServerIP = "109.232.208.203";
            ftpServerIP = "ftp.tmdn.org";
     
           //ftpServerIP = "10.199.2.41";
            //ftpRemotePath = "/tmview/";
            //ftpUserID = "ftpusers";
            //ftpPassword = "FTP-user";
            //ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
            
            //ftpServerIP = "109.232.208.203";
            
            //ftpRemotePath = "/utest/";
            
            ftpUserID = "IPO_INftpusr";
            ftpPassword = "+F6aL_<d";
            ftpURI = "ftp://" + ftpServerIP + "/" + ftpRemotePath + "/";
     
            rootf = "D:/";
            //datestr = "2016-00-00";

            xmlorimage = "xml";
            //filestoupload = "D:/2016-11-22-forwarded/zipped/xmls/";
            String[] filePaths = Directory.GetFiles(filestoupload);
     //String[] filePaths = Directory.GetFiles("D:/2016-11-22-forwarded/zipped/xmls/");
    
     //String[] filesatserver = Directory.GetFiles("D:/2016-11-22-forwarded/zipped/xmls/");// not in use 
     int counfolder = 0;
     int loadfolder=0;

     foreach (string filepath in filePaths)
     {
         FileInfo flinfo = new FileInfo(filepath);
         string urii = ftpURI + flinfo.Name;
         FtpWebRequest rqst;
         rqst = (FtpWebRequest)FtpWebRequest.Create(new Uri(urii));
         rqst = (FtpWebRequest)FtpWebRequest.Create(new Uri(urii));
         rqst.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
         rqst.KeepAlive = false;
         if (File.Exists(urii))
         {
             MessageBox.Show("yes");
                     }
         else 
         { 
         }

         counfolder++;
     }

     textBox4.Text = counfolder.ToString();
     textBox4.Refresh();

   //  MessageBox.Show("next");
     
     foreach (string filepath in filePaths)
            {
            
            repeat:
                FileInfo fileInf = new FileInfo(filepath);
                string uri = ftpURI + fileInf.Name;
                FtpWebRequest reqFTP;
                //retry = false;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri(uri));
                reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.UploadFile;
                reqFTP.UseBinary = true;
                //reqFTP.UsePassive = false;
                reqFTP.ContentLength = fileInf.Length;
                int buffLength = 2048;
                byte[] buff = new byte[buffLength];
                int contentLen;
                FileStream fs = fileInf.OpenRead();
                try
                {
                    Stream strm = reqFTP.GetRequestStream();
                    contentLen = fs.Read(buff, 0, buffLength);
                    while (contentLen != 0)
                    {
                        strm.Write(buff, 0, contentLen);
                        contentLen = fs.Read(buff, 0, buffLength);
                    }
            //        MessageBox.Show("file writing over");
                    strm.Close();
                    fs.Close();
                }
                catch (Exception ex)
                {
                    //Insert_Standard_ErrorLog.Insert("FtpWeb", "Upload Error --> " + ex.Message);
                    
                    //if (!retry)
                    //{
                    //    retry = true;
                    //}
                    //else {
                    //    retry = false;
                    //}
                    goto repeat;
                   //     MessageBox.Show(ex.Message);
                   
                }
               // Console.Write(filepath.ToString());
                loadfolder++;
                textBox1.Text = loadfolder.ToString();
                textBox1.Refresh();
               
            }// end of for loop foreach filepath

            
 //    MessageBox.Show("done");
        }

        
        public Form1()
        {
            InitializeComponent();
            dateTimePicker1.CustomFormat = "dd-mmm-yyyy";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
          // button1_click_duplicate();
          // Application.Exit();

        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            stdate = DateTime.Now;
          //  int appno = 0;

            string monthnumberstr = "";
            string daynumberstr = "";

            if (stdate.Month <= 9)
            {
                monthnumberstr = "0" + stdate.Month.ToString();

            }
            else
            {
                monthnumberstr = stdate.Month.ToString();

            }


            if (stdate.Day <= 9)
            {

                daynumberstr = "0" + stdate.Day.ToString();
            }
            else
            {

                daynumberstr = stdate.Day.ToString();
            }



            datestr = stdate.Year.ToString() + "-" + monthnumberstr + "-" + daynumberstr.ToString(); // temp paused 


            rootf = "D:/";
            datef = datestr + "/";
            string sourcepath = "";
            string destinationpath = "";

            sourcepath=rootf+datestr+"/zipped/images/";
            destinationpath = "/Images/";

            if (System.IO.Directory.Exists(sourcepath))
            {
                Upload(sourcepath, destinationpath, "c");
                MessageBox.Show("image uploaded");
            }
            else
            {
                MessageBox.Show("Nothing today to upload");
            }

            sourcepath = rootf + datestr + "/zipped/xmls/";
            destinationpath = "/TM-XML/";


            if (System.IO.Directory.Exists(sourcepath))
            {
                Upload(sourcepath, destinationpath, "c");
                MessageBox.Show("xml uploaded");
            }
            else
            {
                MessageBox.Show("Nothing today to upload");
            }
            MessageBox.Show("Work Done");
        
        }

        public void button1_click_duplicate()
        {
            stdate = DateTime.Now;
            //  int appno = 0;

            string monthnumberstr = "";
            string daynumberstr = "";

            if (stdate.Month <= 9)
            {
                monthnumberstr = "0" + stdate.Month.ToString();

            }
            else
            {
                monthnumberstr = stdate.Month.ToString();

            }


            if (stdate.Day <= 9)
            {

                daynumberstr = "0" + stdate.Day.ToString();
            }
            else
            {

                daynumberstr = stdate.Day.ToString();
            }



            datestr = stdate.Year.ToString() + "-" + monthnumberstr + "-" + daynumberstr.ToString(); // temp paused 


            rootf = "D:/";
            datef = datestr + "/";
            string sourcepath = "";
            string destinationpath = "";

            sourcepath = rootf + datestr + "/zipped/images/";
            destinationpath = "/Images/";

            if (System.IO.Directory.Exists(sourcepath))
            {
                Upload(sourcepath, destinationpath, "c");
              //  MessageBox.Show("image uploaded");
            }
            else
            {
            //    MessageBox.Show("Nothing today to upload");
            }

            sourcepath = rootf + datestr + "/zipped/xmls/";
            destinationpath = "/TM-XML/";


            if (System.IO.Directory.Exists(sourcepath))
            {
                Upload(sourcepath, destinationpath, "c");
                //MessageBox.Show("xml uploaded");
            }
            else
            {
               // MessageBox.Show("Nothing today to upload");
            }
            //MessageBox.Show("Work Done");
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            //Upload("a", "b", "c");
            // dummy argument passed 

            stdate = DateTime.Now;
            //  int appno = 0;

            string monthnumberstr = "";
            string daynumberstr = "";

            if (stdate.Month <= 9)
            {
                monthnumberstr = "0" + stdate.Month.ToString();

            }
            else
            {
                monthnumberstr = stdate.Month.ToString();

            }


            if (stdate.Day <= 9)
            {

                daynumberstr = "0" + stdate.Day.ToString();
            }
            else
            {

                daynumberstr = stdate.Day.ToString();
            }



            datestr = stdate.Year.ToString() + "-" + monthnumberstr + "-" + daynumberstr.ToString();
            
       String[] filePaths = Directory.GetFiles("D:/"+datestr+"/zipped/images/");
            
           
     
     foreach (string filepath in filePaths)
     {
         bool existit = false;
         //existit = CheckIfFileExistsOnServer("2016-12-19-IN-DIFF-INDX(IMGA)-0001.zip");
         
         //existit = CheckIfFileExistsOnServer(filepath);
         existit = CheckIfFileExistsOnServer(Path.GetFileName(filepath));
         if (existit)
         {
             MessageBox.Show("EXist");
         }
         else
         {
             MessageBox.Show("Do not EXist");
         }
     }


           


      }
        private bool CheckIfFileExistsOnServer(string fileName)
        {
            //var request = (FtpWebRequest)WebRequest.Create("ftp://109.232.208.203/Images/" + fileName);
            var request = (FtpWebRequest)WebRequest.Create("ftp://ftp.tmdn.org/Images/" + fileName);
            
            request.Credentials = new NetworkCredential("IPO_INftpusr", "+F6aL_<d");
            request.Method = WebRequestMethods.Ftp.GetFileSize;

            try
            {
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                return true;
            }
            catch (WebException ex)
            {
                FtpWebResponse response = (FtpWebResponse)ex.Response;
                if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    return false;
            }
            return false;
        }
         

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            
           
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
          
           
         }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            

        }

        private void button3_Click(object sender, EventArgs e)
        {
            int size = -1;
            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK) // Test result.
            {
                string file = openFileDialog1.FileName;

                try
                {
                    string text = File.ReadAllText(file);
                    size = text.Length;
                }
                catch (IOException)
                {
                }
            }
            //Console.WriteLine(size); // <-- Shows file size in debugging mode.
            //Console.WriteLine(result); // <-- For debugging use.

            MessageBox.Show(Path.GetDirectoryName(openFileDialog1.FileName));
            //Path.GetDirectoryName(openFileDialog1.FileName);

            ////-- another 

            //String[] filePaths = Directory.GetFiles("C:/06dec2016/2016-12-6-IN-DIFF-INDX(IMGA)-0001/");
            
            

            //foreach (string filepath in filePaths)
            //{
            //    FileInfo flinfo = new FileInfo(filepath);
            //    int ln = 0;
            //    string nameofile =flinfo.Name;
            //    ln = flinfo.Name.Length-6;

            //   // nameofile = flinfo.Name.Substring(0, 8) + "0" + flinfo.Name.Substring(6,ln);
            //    nameofile = flinfo.FullName.Substring(0, 55) + "0" + flinfo.FullName.Substring(55, flinfo.FullName.Length - 55);

            //    File.Move(flinfo.FullName,nameofile);
            //    MessageBox.Show(nameofile);
               

            //}

          
        }
             
    }
}
