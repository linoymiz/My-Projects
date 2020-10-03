using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C18_Ex05
{
    public class PreGameParameters
    {
        private string[] m_Names = new string[2];
        private bool m_IsAgainstCPU;
        private int m_ColumnSize;
        private int m_RowsSize;

        public PreGameParameters(string[] i_PlayerNames, bool i_IsAgainstComputer, decimal[] i_Dimenstions)
        {
            m_Names[0] = i_PlayerNames[0];
            m_IsAgainstCPU = i_IsAgainstComputer;
            if (m_IsAgainstCPU)
            {
                m_Names[1] = "CPU";
            }
            else
            {
                m_Names[1] = i_PlayerNames[1];
            }

            m_RowsSize = (int)i_Dimenstions[0];
            m_ColumnSize = (int)i_Dimenstions[1];
        }

        public int Row
        {
            get { return m_RowsSize; }
            set { m_RowsSize = value; }
        }

        public int Column
        {
            get { return m_ColumnSize; }
            set { m_ColumnSize = value; }
        }

        public bool CPU
        {
            get { return m_IsAgainstCPU; }
            set { m_IsAgainstCPU = value; }
        }

        public string[] GetPlayersNames()
        {
            return m_Names;
        }
    }
}
