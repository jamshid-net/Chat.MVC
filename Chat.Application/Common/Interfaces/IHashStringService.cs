using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Application.Common.Interfaces;
public interface IHashStringService
{
    Task<string> GetHashStringAsync(string text);
}
