using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace day_17
{
    public partial class Form1 : Form
    {
        public Dictionary<string, string> idPw = new Dictionary<string, string>();
        public Dictionary<string, string> idTel = new Dictionary<string, string>();

        public Form1()
        {
            InitializeComponent();

            // 1. OpenFileDialog를 활용해 사용자 정보 불러오기
            var fileContent = string.Empty;
            var filePath = string.Empty;

            // 2. 2개의 제네릭 딕셔너리를 만들고
            

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    filePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    var fileStream = openFileDialog.OpenFile();

                    using (StreamReader reader = new StreamReader(fileStream))
                    {
                        //fileContent = reader.ReadToEnd();

                        string line;
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] data = line.Split(',');
                            // 2-1. 아이디, 비밀번호
                            // 2-2. 아이디, 전화번호 저장. (전화번호가 없다면 null 저장)

                            string id = data[0];
                            string pw = data[1];
                            string tel = null;

                            if (data.Length >= 3 && !String.IsNullOrEmpty(data[2]))
                            {
                                tel = data[2];
                            }

                            idPw.Add(id, pw);
                            idTel.Add(id, tel);
                        }
                    }
                }
            }

            //MessageBox.Show(fileContent, "File Content at path: " + filePath, MessageBoxButtons.OK);

            // 2-1. 아이디, 비밀번호
            // 2-2. 아이디, 전화번호 저장. (전화번호가 없다면 null 저장)
            // 3. 로그인 창을 만들고 로그인 성공/실패 메시지 박스 띄우기
            // 3-1. 로그인 성공시 메시지박스에 아이디, 전화번호도 띄우기
            // 3-2. 실패 시 메시지박스에 실패 이유 보여주기
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            if (idPw.ContainsKey(textBox_ID.Text))
            {
                if (textBox_PW.Text == idPw[textBox_ID.Text])
                {
                    string telNum = idTel[textBox_ID.Text];
                    if (telNum == null)
                    {
                        MessageBox.Show($"{textBox_ID.Text}님의 전화번호가 없습니다.", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show($"{textBox_ID.Text}님의 전화번호는 {telNum}입니다.", "로그인 성공", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }

                }
                else if (textBox_PW.Text != idPw[textBox_ID.Text])
                {
                    MessageBox.Show("비밀번호가 일치하지 않습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("아이디가 존재하지 않습니다.", "로그인 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }
    }
}
