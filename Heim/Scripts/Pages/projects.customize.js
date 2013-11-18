jQuery(document).ready(function () {

	(function ($) {
		$('.floor-variant[data-id]').click(function () {
			var variant = $(this);

			variant.closest('ul').find('.floor-variant[data-id]').removeClass('selected');
			variant.addClass('selected');

			variant.closest('.floor').find('.plan-preview img').attr('src', variant.find('img').attr('src'));
		});

		$('a.new-project').click(function (event) {

			var selectedVariants = [];

			$('#floors .floor-variant.selected').each(function () {
				var item = $(this);

				selectedVariants.push({
					ID: item.attr('data-id'),
					floor: item.closest('.floor').attr('data-floor'),
					name: item.find('.name').text(),
					previewImageUrl: item.find('img').attr('src'),
					FloorNumber: 1
				});
			});

			var vmProject = {
				name: _model.name,
				planTemplateId: _model.planId,
				previewImageUrl: _model.previewImageUrl,
				floors: selectedVariants
			};

			var html = _.template(
					_.findTemplate('create-project-dialog'),
					vmProject
				);

			var height = (vmProject.floors.length > 2) ? 675 : 490;

			var isPosting = false;

			$('#create-project').html(html).dialog({
				width: 795,
				height: height,
				modal: true,
				resizable: false,
				open: function () {
					var input_name = $('input[type=text][name=projectName]');
					input_name.removeClass('input-validation-error');
				},
				title: "Create project",
				buttons: {
					'Create and go to next step': function () {

						if (!isPosting) {
							isPosting = true;

							var input_name = $('input[type=text][name=projectName]');
							input_name.removeClass('input-validation-error');

							if ($.trim(input_name.val()) === '') {
								input_name.addClass('input-validation-error').focus();

							} else {

								$('.ui-dialog .ui-button-text').html('Creating ' + vmProject.name);

								vmProject.name = input_name.val();

								var dlg = $(this);

								$.post("/Projects/SaveCustomize", vmProject).success(function (v) {
									document.location = "/Design/Exterior/" + v.ID;
								}).error(function (err) {

									$('.ui-dialog .ui-button-text').html('Opps! Error.');

								}).done(function () {
									isPosting = false;
								});
							}
						} else {
							///
						}
					}
				}
			});

			event.stopPropagation();

			return false;
		});

	})(jQuery);

});