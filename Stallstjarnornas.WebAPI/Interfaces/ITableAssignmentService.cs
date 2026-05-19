using Microsoft.AspNetCore.Mvc;
using Stallstjarnornas.WebAPI.DTOs.TableAssignment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stallstjarnornas.WebAPI.Interfaces
{
    public interface ITableAssignmentService
    {
        Task<TableAssignmentResponseDto> CreateTableAssignmentAsync(CreateTableAssignmentDto dto);
        Task <GetAvailableTablesResponseDto> GetAvailableTablesAsync(GetAvailableTablesDto dto);
    }
}
