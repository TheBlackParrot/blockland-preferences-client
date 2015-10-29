if(!isObject(BLPrefWindow)) {
	exec("./gui/window.gui");
	exec("./gui/textProfile.gui");
}

function openBLPrefWindow() {
	canvas.pushDialog(BLPrefWindow);
	commandToServer('getBLPrefCategories');
}

//function clientCmdAddPref(%category, %title, %type, %variable, %value, %params, %icon) {
function clientCmdAddCategory(%category, %icon) {
	if($BLPrefs::Client::Exists[%category]) {
		return;
	}

	$BLPrefs::Client::Exists[%category] = true;
	%color = (($BLPrefs::CategoryRows % 2) ? "220 235 255" : "230 245 255") SPC "255";

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
		color = %color;
		originalColor = %color;
		category = %category;

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
	};

	%text = new GuiTextCtrl() {
		profile = "BLPrefTextProfile";
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
	%row.textObj = %text;
	%row.add(%text);

	%button = new GuiBitmapButtonCtrl(BLPrefCategorySwitchButton) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0 0";
		extent = "140 20";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		command = "requestBLPrefCategory(\"" @ %category @ "\");";
		groupNum = "-1";
		buttonType = "PushButton";
		bitmap = "base/client/ui/btnBlank";
		lockAspectRatio = "0";
		alignLeft = "0";
		alignTop = "0";
		overflowImage = "0";
		mKeepCached = "0";
		mColor = "255 255 255 255";
		text = "";
	};
	%row.add(%button);
	%button.setValue("");

	BLPrefCategoryList.add(%row);

	$BLPrefs::CategoryRows++;
}

function requestBLPrefCategory(%category) {
	BLPrefTitle.setValue(%category);
	$BLPrefs::LastRowPos = 20;
	$BLPrefs::PrefRows = 0;
	BLPrefCategoryList.setRowActive(%category);
	BLPrefPrefList.clearPrefRows();
	commandToServer('getBLPrefCategory', %category);
}

function clientCmdReceivePref(%title, %type, %variable, %value, %params, %legacy) {
	if(%legacy) {
		BLPrefTitle.setValue(BLPrefTitle.getValue() SPC "(Legacy)");
	}

	switch$(%type) {
		case "number":
			BLPrefPrefList.addTextInput(%title, %variable, %value, %params);
	}
}

function BLPrefPrefList::clearPrefRows(%this) {
	// we have to go in reverse D:
	for(%i=%this.getCount()-1;%i>=0;%i--) {
		%row = %this.getObject(%i);
		if(%row.isRow) {
			%row.delete();
		}
	}
}

function BLPrefCategoryList::setRowActive(%this, %category) {
	for(%i=0;%i<%this.getCount();%i++) {
		%row = %this.getObject(%i);

		if(%row.category $= %category) {
			%row.color = "143 161 179 255";
			%row.textObj.setProfile(BLPrefTextSelectedProfile);
		} else {
			%row.color = %row.originalColor;
			%row.textObj.setProfile(BLPrefTextProfile);
		}
	}
}