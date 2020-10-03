namespace C18_Ex05
{
    public struct Point
    {
        private int m_Row;//Horizonal axis of board
        private int m_Column;//Vertical axis of board

        public int Row
        {
            set { m_Row = value; }
            get { return m_Row; }
        }

        //Sets and gets X

        public int Column
        {
            set { m_Column = value; }
            get { return m_Column; }
        }

        //Sets and gets Y

        public void SetPoint(int horizonalAxis, int VerticalAxis)
        {
            Row = horizonalAxis;
            Column = VerticalAxis;
        }
        //Sets both

    }
}