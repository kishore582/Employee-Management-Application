using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebApplication.EmployeeSchema;
using WebApplication.EmployeeDAL;

namespace WebApplication.EmployeeBAL
{
    public class Class1
    {
        public int Insert(EmployeeSchema.Class1 objSchema)
        {
            try
            {
                EmployeeDAL.Class1 objDAL = new EmployeeDAL.Class1();
                return objDAL.InsertData(objSchema);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Update(EmployeeSchema.Class1 objSchema, int Id)
        {
            try
            {
                EmployeeDAL.Class1 objDAL = new EmployeeDAL.Class1();
                return objDAL.UpdateData(objSchema, Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int Delete(int Id)
        {
            try
            {
                EmployeeDAL.Class1 objDAL = new EmployeeDAL.Class1();
                return objDAL.DeleteData(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable BindGrid()
        {
            try
            {
                EmployeeDAL.Class1 objDAL = new EmployeeDAL.Class1();
                return objDAL.BindGrid();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetById(int Id)
        {
            try
            {
                EmployeeDAL.Class1 objDAL = new EmployeeDAL.Class1();
                return objDAL.GetById(Id);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}