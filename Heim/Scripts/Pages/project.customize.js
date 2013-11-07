jQuery(document).ready(function () {

	(function ($) {

		$('a.new-project').click(function (event) {

			var project = {
				name: "SMART S1",
				floors: [
						{
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

			$('section[name=create-project]').html(html).dialog({
				width: 880,
				height: 600,
				modal: true,

				title: "Create project",
				buttons: {
					'Create and go to next step': function(){
						alert('created');
						$(this).dialog('close');
					}
				}
			});

			event.stopPropagation();

			return false;
		});

	})(jQuery);

});