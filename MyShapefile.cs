using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using GeoAPI;
using System.Data;

namespace Map2SQL
{
    public class MyShapefile
    {
        DataAccess da = new DataAccess();
        FontConverter cv = new FontConverter();
        // Thuoc tinh
        public String FileName { get; set; }
        public String SQLTableName { get; set; }

        // Phuong thuc
        public int UpdateToDB()
        {
            try
            {
                GeometryServiceProvider.Instance = new NetTopologySuite.NtsGeometryServices();

                // Mo tep tin
                SharpMap.Data.Providers.ShapeFile map = new SharpMap.Data.Providers.ShapeFile(FileName);
                map.Open();

                String dsCot = "";
                int demKQ = 0;

                // Doc cau truc cua tep tin
                var feature = map.GetFeature(0);
                String query = "CREATE TABLE " + SQLTableName + "(ID int Identity(1,1) PRIMARY KEY, ";
                foreach(DataColumn col in feature.Table.Columns)
                {
                    if (col.ColumnName.ToLower().Equals("id") || col.ColumnName.ToLower().Equals("oid"))
                        continue;
                    dsCot += col.ColumnName + ", ";

                    String type = col.DataType.ToString().ToLower().Split('.')[1];
                    if (type.Equals("string"))
                        query += col.ColumnName + " nvarchar(max), ";
                    else if (type.Equals("int16") || type.Equals("int32") || type.Equals("int64")
                        || type.Equals("uint16") || type.Equals("uint32") || type.Equals("uint64"))
                        query += col.ColumnName + " int, ";
                    else if (type.Equals("float") || type.Equals("double"))
                        query += col.ColumnName + " float, ";
                }
                dsCot += "geom";
                query += "geom Geometry)";
                da.Write(query);

                // Doc du lieu tren ban do va cap nhat vao CSDL
                // INSERT INTO tenBang(ds cot) VALUES(ds gia tri)
                for(int i = 0; i < map.GetFeatureCount(); i++)
                {
                    var featurei = map.GetFeature((uint)i);
                    String insertQuery = "INSERT INTO " + SQLTableName + "(" + dsCot
                        + ") VALUES(";
                    String values = "";
                    foreach(DataColumn col in featurei.Table.Columns)
                    {
                        if (col.ColumnName.ToLower().Equals("oid") || col.ColumnName.ToLower().Equals("id"))
                            continue;
                        String type = col.DataType.ToString().ToLower().Split('.')[1];
                        if(type.ToLower().Equals("string"))
                        {
                            values += "N'" + cv.TCVN3ToUnicode(featurei.ItemArray[col.Ordinal].ToString()) + "', ";
                        }
                        else
                        {
                            if (featurei.ItemArray[col.Ordinal].ToString().Length > 0)
                                values += featurei.ItemArray[col.Ordinal].ToString() + ", ";
                            else
                                values += "0, ";
                        }
                    }
                    String polygon = featurei.Geometry.ToString();
                    values += "Geometry::STPolyFromText('" + polygon + "', 4326)";
                    insertQuery += values + ")";
                    demKQ += da.Write(insertQuery);                    
                }
                return demKQ;
            }
            catch
            {
                return 0;
            }
        }
    }
}
