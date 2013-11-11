jQuery(document).ready(function () {

	(function ($) {
		$('.floor-variant[data-option]').click(function () {
			var variant = $(this);

			variant.closest('ul').find('.floor-variant[data-option]').removeClass('selected');
			variant.addClass('selected');

			variant.closest('.floor').find('.plan-preview img').attr('src', variant.find('img').attr('src'));
		});

		$('a.new-project').click(function (event) {

			var project = {
				name: "SMART S1",
				floors: [{

					},
				]
			};

			var vmProject = {
				planId: 1111,
				previewImageUrl: '/UserData/scg_th01_04floorplan-1.jpg',
				planName: 'SMART S1',
				floors: [
					{
						name: 'Option 2',
						floorName: '1F',
						previewImageUrl: '/UserData/scg_th01_04floorplan-1.png'
					},

					{
						name: 'Option 1',
						floorName: '2F',
						previewImageUrl: '/UserData/scg_th01_04floorplan-2.png'
					},

					{
						name: 'Option 3',
						floorName: '3F',
						previewImageUrl: '/UserData/scg_th01_04floorplan-1.png'
					}
				]
			};
						
			var html = _.template(
					_.findTemplate('create-project-dialog'),
					vmProject
				);

			var height = (vmProject.floors.length > 2)? 675 : 490;

			$('section[name=create-project]').html(html).dialog({
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

						var input_name = $('input[type=text][name=projectName]');
						input_name.removeClass('input-validation-error');

						if ($.trim(input_name.val()) === '') {
							input_name.addClass('input-validation-error').focus();

						} else {


							$(this).dialog('close');
						}
					}
				}
			});

			event.stopPropagation();

			return false;
		});

	})(jQuery);

});