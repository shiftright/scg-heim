if (window.SR === undefined) {
	window.SR = {
		Heim: {}
	};
}

(function ($, SR, Unity) {

	// Unity instance
	var _u = null;

	// designer instance
	var _designer = null;

	SR.Heim.Designer = {
		_model: {},
		_url: 'http://localhost:5630/Content/Demo.unity3d',

		init: function (selector, options) {

			if (Unity === undefined) {
				throw "UnityObject2 is missing";
			}

			_u = Unity({
				params: { disableContextMenu: true },
				enableUnityAnalytics: false,
				enableGoogleAnalytics: false
			});

			_u.observeProgress(_designer.onUnityProgress);
			_u.initPlugin($(selector).get(0), _designer._url);

			return _designer;
		},

		onReady: function () {

		},

		onUnityProgress: function (progress) {
			var $missingScreen = $(progress.targetEl).find(".missing");
			switch (progress.pluginStatus) {
				case "unsupported":
					alert("Unity web player cannot run on this browser :(");
					break;
				case "broken":
					alert("You will need to restart your browser after installation.");
					break;
				case "missing":
					$missingScreen.find("a").click(function (e) {
						e.stopPropagation();
						e.preventDefault();
						u.installPlugin();
						return false;
					});
					$missingScreen.show();
					break;
				case "installed":
					$missingScreen.remove();
					break;
				case "first":
					break;
			}
		},

		/// Send message to Unity
		sendMessage: function (objectName, funcName, params) {
			if (_u) {
				var uni = _u.getUnity();
				if (uni) {
					return uni.SendMessage(objectName, funcName, params);
				} else {
					throw "The game might not fully loaded yet";
				}
			} else {
				throw "Unity engine must be initialize first";
			}
		},

		/// Dispatch message from unity
		message: function (messageName, params) {
			
			switch (messageName.toLowerCase()) {
				case 'ready': _designer.onReady(); break;
				case 'echo': console.log(params); break;

				default: break;
			}
		},

		loadModel: function (model) {
			return _designer.sendMessage('CommandInterface', 'loadModel', model);
		},

		setRoofTile: function (floorNumber, assetName) {
			return _designer.sendMessage('roofTile_' + floor, 'setAsset', assetName);
		},

		addFurniture: function (floorNumber, assetName, position) {
			return _designer.sendMessage('floor_' + floorNumber, 'addFurniture', assetName, position);
		},

		updateFurniture: function (floorNumber, assetName, position) {
			return _designer.sendMessage('floor_' + floorNumber, 'updateFurniture', assetName, position);
		},

		destroyAsset: function (assetName) {
			return _designer.sendMessage(assetName, 'destroy');
		}
	};

	_designer = SR.Heim.Designer;
	window.$d = _designer;

})(jQuery, SR, UnityObject2);