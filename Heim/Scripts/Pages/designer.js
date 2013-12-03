(function ($) {
	$(document).ready(function () {

		var costSummaryPopup = null;

		$('body').click(function () {
			if (costSummaryPopup) {
				costSummaryPopup.fadeOut(100, function () {
					costSummaryPopup.remove();
					costSummaryPopup = null;
				});
				
			}
		});

		$('.step-cost-summary').click(function () {
			var a = $(this);

			if (costSummaryPopup) {
				costSummaryPopup.remove();
				costSummaryPopup = null;
			}

			$.get('/Design/CostSummary', null, function (resp, status, xhr) {
				if (status == 'success') {
					a.parent().append('<div class="dialog">' + resp + '</div>');
					costSummaryPopup = a.parent().find('>.dialog');
					costSummaryPopup.click(function (event) {
						event.stopPropagation();
					});
				}
			});
		});
	});
})(jQuery);