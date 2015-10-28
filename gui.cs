if(!isObject(BLPrefWindow)) {
	exec("./gui/window.gui");
}

function openBLPrefWindow() {
	canvas.pushDialog(BLPrefWindow);
	commandToServer('getBLPrefs');
}

function clientCmdAddPref(%category, %title, %type, %variable, %value, %params, %icon) {
	if($BLPrefs::Client::Exists[%category]) {
		return;
	}

	$BLPrefs::Client::Exists[%category] = true;
	%row = new GuiSwatchCtrl(BLPrefCategoryRow) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC $BLPrefs::CategoryRows*20;
		extent = "142 20";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = (($BLPrefs::CategoryRows % 2) ? "220 235 255" : "230 245 255") SPC "255";

		new GuiBitmapCtrl() {
			profile = "GuiDefaultProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "4 1";
			extent = "16 16";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			bitmap = "Add-Ons/Client_BlocklandPreferences/icons/" @ getIconOverride(%category, %icon) @ ".png";
			wrap = "0";
			lockAspectRatio = "1";
			alignLeft = "0";
			alignTop = "0";
			overflowImage = "0";
			keepCached = "0";
			mColor = "255 255 255 255";
			mMultiply = "0";
		};
		new GuiTextCtrl() {
			profile = "GuiTextProfile";
			horizSizing = "right";
			vertSizing = "bottom";
			position = "24 1";
			extent = "44 18";
			minExtent = "8 2";
			enabled = "1";
			visible = "1";
			clipToParent = "1";
			text = %category;
			maxLength = "255";
		};
	};
	BLPrefCategoryList.add(%row);
	$BLPrefs::CategoryRows++;
}