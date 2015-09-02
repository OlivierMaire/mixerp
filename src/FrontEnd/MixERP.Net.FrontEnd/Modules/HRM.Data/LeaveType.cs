/********************************************************************************
Copyright (C) MixERP Inc. (http://mixof.org).

This file is part of MixERP.

MixERP is free software: you can redistribute it and/or modify
it under the terms of the GNU General Public License as published by
the Free Software Foundation, version 2 of the License.


MixERP is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
GNU General Public License for more details.

You should have received a copy of the GNU General Public License
along with MixERP.  If not, see <http://www.gnu.org/licenses/>.
***********************************************************************************/
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MixERP.Net.DbFactory;
using Npgsql;
using PetaPoco;

namespace MixERP.Net.Core.Modules.HRM.Data
{
    public class LeaveType
    {
		/// <summary>
		/// Performs SQL count on the table "hrm.leave_types".
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <returns>Returns the number of rows of the table "hrm.leave_types".</returns>
		public long Count(string catalog)
		{
			const string sql = "SELECT COUNT(*) FROM hrm.leave_types;";
			return Factory.Scalar<long>(catalog, sql);
		}

		/// <summary>
		/// Executes a select query on the table "hrm.leave_types" with a where filter on the column "leave_type_id" to return a single instance of the "LeaveType" class. 
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <param name="leaveTypeId">The column "leave_type_id" parameter used on where filter.</param>
		/// <returns>Returns a non-live, non-mapped instance of "LeaveType" class mapped to the database row.</returns>
		public MixERP.Net.Entities.HRM.LeaveType Get(string catalog, int leaveTypeId)
		{
			const string sql = "SELECT * FROM hrm.leave_types WHERE leave_type_id=@0;";
			return Factory.Get<MixERP.Net.Entities.HRM.LeaveType>(catalog, sql, leaveTypeId).FirstOrDefault();
		}

        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		public static IEnumerable<DisplayField> GetDisplayFields(string catalog)
		{
			List<DisplayField> displayFields = new List<DisplayField>();

			const string sql = "SELECT leave_type_id AS key, leave_type_code || ' (' || leave_type_name || ')' as value FROM hrm.leave_types;";
			using (NpgsqlCommand command = new NpgsqlCommand(sql))
			{
				using (DataTable table = DbOperation.GetDataTable(catalog, command))
				{
					if (table?.Rows == null || table.Rows.Count == 0)
					{
						return displayFields;
					}

					foreach (DataRow row in table.Rows)
					{
						if (row != null)
						{
						    DisplayField displayField = new DisplayField
						    {
						        Key = row["key"].ToString(),
						        Value = row["value"].ToString()
						    };

							displayFields.Add(displayField);
						}
					}
				}
			}

			return displayFields;
		}

		/// <summary>
		/// Inserts the instance of LeaveType class on the database table "hrm.leave_types".
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <param name="leaveType">The instance of "LeaveType" class to insert.</param>
		public void Add(string catalog, MixERP.Net.Entities.HRM.LeaveType leaveType)
		{
			Factory.Insert(catalog, leaveType);
		}

		/// <summary>
		/// Updates the row of the table "hrm.leave_types" with an instance of "LeaveType" class against the primary key value.
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <param name="leaveType">The instance of "LeaveType" class to update.</param>
		/// <param name="leaveTypeId">The value of the column "leave_type_id" which will be updated.</param>
		public void Update(string catalog, MixERP.Net.Entities.HRM.LeaveType leaveType, int leaveTypeId)
		{
			Factory.Update(catalog, leaveType, leaveTypeId);
		}

		/// <summary>
		/// Deletes the row of the table "hrm.leave_types" against the primary key value.
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <param name="leaveTypeId">The value of the column "leave_type_id" which will be deleted.</param>
		public void Delete(string catalog, int leaveTypeId)
		{
			const string sql = "DELETE FROM hrm.leave_types WHERE leave_type_id=@0;";
			Factory.NonQuery(catalog, sql, leaveTypeId);
		}

		/// <summary>
		/// Performs a select statement on table "hrm.leave_types" producing a paged result of 10.
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <returns>Returns the first page of collection of "LeaveType" class.</returns>
		public IEnumerable<MixERP.Net.Entities.HRM.LeaveType> GetPagedResult(string catalog)
		{
			const string sql = "SELECT * FROM hrm.leave_types ORDER BY leave_type_id LIMIT 10 OFFSET 0;";
			return Factory.Get<MixERP.Net.Entities.HRM.LeaveType>(catalog, sql);
		}

		/// <summary>
		/// Performs a select statement on table "hrm.leave_types" producing a paged result of 10.
		/// </summary>
        /// <param name="catalog">The name of the database on which queries are being executed to.</param>
		/// <param name="pageNumber">Enter the page number to produce the paged result.</param>
		/// <returns>Returns collection of "LeaveType" class.</returns>
		public IEnumerable<MixERP.Net.Entities.HRM.LeaveType> GetPagedResult(string catalog, long pageNumber)
		{
			long offset = (pageNumber -1) * 10;
			const string sql = "SELECT * FROM hrm.leave_types ORDER BY leave_type_id LIMIT 10 OFFSET @0;";
				
			return Factory.Get<MixERP.Net.Entities.HRM.LeaveType>(catalog, sql, offset);
		}
	}
}