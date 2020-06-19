using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIServer.Filters
{
    public class _basisParameters
    {
		const int _pageSizeMax = 50;
		private int _pageSizeField = 10;
		private int _pageNumberField = 1;
		private int _pageNumberMaxField = 1;

		private string _search { get; set; }
		public int _pageSize
		{
			get { return _pageSizeField; }
			set { _pageSizeField = (value > _pageSizeMax) ? _pageSizeMax : (value < 1) ? 1 : value; }
		}
		public int _pageNumber
		{
			get { return _pageNumberField; }
			set { _pageNumberField = (value < 1) ? 1 : value; }
		}
		public int _pageNumberMax
		{
			get { return _pageNumberMaxField; }
			set { _pageNumberMaxField = (value < 1) ? 1 : value; }
		}
	}
}
