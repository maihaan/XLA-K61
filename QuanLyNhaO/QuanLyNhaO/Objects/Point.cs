using GeoAPI;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;

namespace QuanLyNhaO.Objects
{
    public class Point
    {
        public int ID { get; set; }
        public String Ten { get; set; }
        public List<PointF> Geom { get; set; }

        DataAccess da = new DataAccess();

        public int UpdateToDB(String fileName)
        {
            try
            {
                GeometryServiceProvider.Instance = new NetTopologySuite.NtsGeometryServices();

                // Mo tep tin
                SharpMap.Data.Providers.ShapeFile map = new SharpMap.Data.Providers.ShapeFile(fileName);
                map.Open();

                String dsCot = "";
                int demKQ = 0;

                // Doc cau truc cua tep tin
                var feature = map.GetFeature(0);
                foreach (DataColumn col in feature.Table.Columns)
                {
                    if (col.ColumnName.ToLower().Equals("id") || col.ColumnName.ToLower().Equals("oid"))
                        continue;
                    dsCot += col.ColumnName + ", ";
                }
                dsCot += "geom";

                // Doc du lieu tren ban do va cap nhat vao CSDL
                // INSERT INTO tenBang(ds cot) VALUES(ds gia tri)
                for (int i = 0; i < map.GetFeatureCount(); i++)
                {
                    var featurei = map.GetFeature((uint)i);
                    String insertQuery = "INSERT INTO tbPoint(" + dsCot
                        + ") VALUES(";
                    String values = "";
                    foreach (DataColumn col in featurei.Table.Columns)
                    {
                        if (col.ColumnName.ToLower().Equals("oid") || col.ColumnName.ToLower().Equals("id"))
                            continue;
                        String type = col.DataType.ToString().ToLower().Split('.')[1];
                        if (type.ToLower().Equals("string"))
                        {
                            values += "N'" + featurei.ItemArray[col.Ordinal].ToString() + "', ";
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
                    values += "Geometry::STPointFromText('" + polygon + "', 4326)";
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

        public List<Point> Select(String condition)
        {
            String query = "Select ID, Ten, Geom.ToString() As Geom From tbPoint";
            if (!String.IsNullOrEmpty(condition))
                query += " Where " + condition;
            DataTable tb = da.Read(query);
            if (tb != null && tb.Rows.Count > 0)
            {
                List<Point> ds = new List<Point>();
                foreach (DataRow r in tb.Rows)
                {
                    Point p = new Point();
                    p.ID = int.Parse(r["ID"].ToString());
                    p.Ten = r["Ten"].ToString();
                    String geoData = r["Geom"].ToString();
                    geoData = geoData.Substring(10, geoData.Length - 12);
                    foreach (String pointData in geoData.Split(','))
                    {
                        PointF po = new PointF();
                        po.X = float.Parse(pointData.Trim().Split(' ')[0]);
                        po.Y = float.Parse(pointData.Trim().Split(' ')[1]);
                        p.Geom.Add(po);
                    }
                    ds.Add(p);
                }
                return ds;
            }
            else
                return null;
        }
    }
}