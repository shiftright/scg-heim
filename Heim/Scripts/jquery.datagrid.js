
(function ($) {

	//$.datagrid({
	//	title: 'cost summary',
	//	columns: ['งาน', 'ค่าโน่น', 'ค่านี่', 'รวม'],
	//	items: [
	//		['งานโน้น', 'xxx', 'xxx', 'xxx'],
	//		['งาน', 'ค่าโน่น', 'ddd', 'รวม'],

	//	]
	//});

	$.extend($.fn, {
		datagrid: function (options) {

			//// if nothing is selected, return nothing; can't chain anyway
			//if (!this.length) {
			//	if (options && options.debug && window.console) {
			//		console.warn("Nothing selected, can't validate, returning nothing.");
			//	}
			//	return;
			//}

			//if (!!options.data) {
			//	console.warn("No data!!");
			//}

			//return this.append($.fn._datagrid_renderTable(options.data, options));
		},

		_datagrid_renderTable: function (data, options, level) {
			//var table = $('<table></table>');

			//if (level === undefined) {
			//	level = 1;
			//}

			//table.attr('data-level', level);

			//if (data.title) {
			//	table.append('<caption>' + data.title + '</caption>');
			//}

			//var thead = table.append('<thead></thead>');
			//var thead_tr = thead.append('<tr></tr>');

			//$.each(data.columns, function (index, col) {
			//	thead_tr.append('<th>'+col+'</th>');
			//});

			//$.each(data.items, function (index, datarow) {
			//	var row = $.fn._datagrid_renderRow(datarow);
			//	table.append(row);

			//	if (typeof item == 'object') {
			//		var subTable = $.fn._datagrid_renderTable(, options, level + 1);
			//		table.append(subTable);
			//	}
			//});

			//return table;
		},

		_datagrid_renderRow: function (datarow) {
			//var htmlString = '';
			//$.each(datarow, function (idx, value) {
			//	htmlString += '<td>' + value + '</td>';
			//});

			//return $('<tr>'+ htmlString +'</tr>');
		}
	});

})(jQuery);