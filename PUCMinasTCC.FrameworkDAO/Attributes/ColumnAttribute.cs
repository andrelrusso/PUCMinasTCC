using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace PUCMinasTCC.FrameworkDAO.Attributes
{
    public class ColumnAttribute : Attribute
    {
        public string Name { get; set; }
        public DbType DataType { get; set; }

        public ColumnAttribute(string name)
        {
            Name = name;
        }

        public ColumnAttribute(string name, DbType dataType)
        {
            Name = name;
            DataType = dataType;
        }
    }
}
