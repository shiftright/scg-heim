﻿if (window.SR === undefined) {
	window.SR = {
		Heim: {}
	};
}

(function ($, SR, Unity) {

	// Unity instance
	var _u = null;

	// designer instance
	var _designer = null;

	var _options = null;

	SR.Heim.Designer = {
		_model: {},
		_url: 'http://localhost:5630/Content/HouseLoader.unity3d',
		_options: {
			onUnityLoaded: null
		},

		init: function (selector, options) {

			if (Unity === undefined) {
				throw "UnityObject2 is missing";
			}

			_designer._options = $.extend(_designer._options, options);

			_u = Unity({
				params: { disableContextMenu: true },
				enableUnityAnalytics: false,
				enableGoogleAnalytics: false
			});

			_u.observeProgress(_designer.onUnityProgress);
			_u.initPlugin($(selector).get(0), _designer._url);
			_designer._bindToolBoxes();

			return _designer;
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

		_bindToolBoxes: function () {
			$('.toolbox li').click(function () {
				var $this = $(this);
				var cmd = 'Set' + $this.parent().attr('data-type');
				var args = {
					name: '',
					file: $this.attr('data-asset')
				}

				_designer.sendMessage(cmd, args);
			});
		},

		sendMessage: function (funcName, args) {
			return _designer.sendObjectMessage("CommandInterface", funcName, args);
		},

		/// Send message to Unity
		sendObjectMessage: function (objectName, funcName, args) {
			if (_u) {
				var uni = _u.getUnity();
				if (uni) {
					console.log(objectName, funcName, args);
					return uni.SendMessage(objectName, funcName, args);
				} else {
					throw "The game might not fully loaded yet";
				}
			} else {
				throw "Unity engine must be initialize first";
			}
		},

		/// Dispatch message from unity
		message: function (messageName, params) {

			console.log(messageName);
			
			switch (messageName.toLowerCase()) {
				case 'ready': {
					if(typeof _options.onUnityLoaded == 'function'){
						_options.onUnityLoaded.apply(_designer);
					}
				}
				case 'echo': console.log(params); break;

				default: break;
			}
		},

		loadHouse: function (model) {
			return _designer.sendMessage('LoadHouse', model);
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
	_options = _designer._options;
	window.$d = _designer;

})(jQuery, SR, UnityObject2);