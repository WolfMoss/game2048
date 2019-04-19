using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace game2048
{
    class Logic
    {

        
        Map map1 = new Map();


        //把地图的状态数据转化为最终显示的一维字符串数组
        private string[] drewsNums = new string[16];
        public string[] DrewsNums
        {
            get { return Drew(); }
            set { drewsNums = value; }
        }
        public string[] Drew()
        {
            //加载最终显示的字符串
            map1.ShowStr = Map.Nums;

            string[] arr = new string[16];
            int box = 0;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {

                    arr[box] = map1.ShowStr[map1.Pos[i, j]];
                    box += 1;
                }
            }
            return arr;
        }

        //生成随机数
        public void Create ()
        {
            int i;
            int j;
            int num;
            Random ran = new Random();
            while (true)
            {
                i = ran.Next(0, 4);
                j = ran.Next(0, 4);
                num = ran.Next(1, 3);
                if (map1.Pos[i, j] == 0)
                {
                    map1.Pos[i, j] = num;

                    return;
                }
            }
        }

        //判断输了没,true=输，false=没输。输的条件是没有空位且所有数字相邻
        public bool Lose()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j]==0)
                    {
                        return false;
                    }
                    else
                    {
                        if (i == 0 && j == 0)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1])
                            {
                                return false;
                            }
                        }
                        else if (i == 3 && j == 0)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1])
                            {
                                return false;
                            }
                        }
                        else if (i == 0 && j == 3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }
                        else if (i == 3 && j == 3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }
                        else if (i!=0&&i!=3&&j!=0&&j!=3)
                        {
                            if (map1.Pos[i, j] == map1.Pos[i + 1, j] || map1.Pos[i, j] == map1.Pos[i - 1, j] || map1.Pos[i, j] == map1.Pos[i, j + 1] || map1.Pos[i, j] == map1.Pos[i, j - 1])
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
        //重新来过的方法
        public void ReGame()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    map1.Pos[i, j] = 0;
                }
            }
            Create();
        }

        //处理输赢的方法，没输就看有没有2048的，有2048就提示赢了，输了就提示输了并重新来过
        public void RunLose()
        {
            if (Lose())
            {
                DialogResult dr = MessageBox.Show("你输了", "结果", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.OK)
                {
                    ReGame();
                }
            }
            else
            {
                for (int i = 0; i < 4; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        if (map1.Pos[i, j] == 11)
                        {
                            MessageBox.Show("你赢了");
                        }
                    }
                }
            }
        }
        
        //能否向上移动
        public bool CanUp()
        {
            for (int i = 1; i < 4; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ( (map1.Pos[i, j] != 0 &&  map1.Pos[i-1, j] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i-1, j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //能否向左移动
        public bool CanLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 1; j < 4; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i, j-1] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i, j-1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //能否向右移动
        public bool CanRight()
        {
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i, j + 1] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i, j + 1]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        //能否向下移动
        public bool CanDown()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if ((map1.Pos[i, j] != 0 && map1.Pos[i+1, j] == 0) || (map1.Pos[i, j] != 0 && map1.Pos[i, j] == map1.Pos[i+1,j]))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        //上事件
        public void Up()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j]!=0)
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[k, j])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[k, j] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = i + 1; k < 4; k++)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[k, j];
                                    map1.Pos[k, j] = 0;
                                }
                                for (int l = k+1; l < 4; l++)
                                {
                                    if (map1.Pos[l, j] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[l, j])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[l, j] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //左事件
        public void Left()
        {
            for (int j = 0; j < 3; j++)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[i, k])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[i, k] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = j + 1; k < 4; k++)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] ==0)
                                {
                                    map1.Pos[i, j] = map1.Pos[i, k];
                                    map1.Pos[i, k] = 0;
                                }
                                
                                for (int l = k + 1; l < 4; l++)
                                {
                                    if (map1.Pos[i, l] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[i, l])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[i, l] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //右事件
        public void Right()
        {
            for (int j = 3; j > 0; j--)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = j - 1; k >=0; k--)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[i, k])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[i, k] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = j - 1; k >= 0; k--)
                        {
                            if (map1.Pos[i, k] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[i, k];
                                    map1.Pos[i, k] = 0;
                                }
                                
                                for (int l = k - 1; l >= 0; l--)
                                {
                                    if (map1.Pos[i, l] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[i, l])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[i, l] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }
        //下事件
        public void Down()
        {
            for (int i = 3; i >0; i--)
            {
                for (int j = 0; j < 4; j++)
                {
                    if (map1.Pos[i, j] != 0)
                    {
                        for (int k = i - 1; k >=0; k--)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == map1.Pos[k, j])
                                {
                                    map1.Pos[i, j] += 1;
                                    map1.Pos[k, j] = 0;
                                }
                                else
                                {
                                    break;
                                }
                            }

                        }
                    }
                    else
                    {
                        for (int k = i - 1; k >=0; k--)
                        {
                            if (map1.Pos[k, j] != 0)
                            {
                                if (map1.Pos[i, j] == 0)
                                {
                                    map1.Pos[i, j] = map1.Pos[k, j];
                                    map1.Pos[k, j] = 0;
                                }
                                
                                for (int l = k - 1; l >=0; l--)
                                {
                                    if (map1.Pos[l, j] != 0)
                                    {
                                        if (map1.Pos[i, j] == map1.Pos[l, j])
                                        {
                                            map1.Pos[i, j] += 1;
                                            map1.Pos[l, j] = 0;
                                        }
                                        else
                                        {
                                            break;
                                        }
                                    }

                                }
                            }
                        }
                    }
                }
            }
        }

    }
}
