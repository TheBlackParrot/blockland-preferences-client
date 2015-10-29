// in case anyone else ever runs into the issue of scrollCtrl's not scrolling, here you go:
// http://forum.blockland.us/index.php?topic=234496.0

$BLPrefs::BaseRowWidth = 432;

function BLPrefPrefList::addTextInput(%this, %title, %variable, %value, %params) {
	%row = new GuiSwatchCtrl(BLPrefTextRow) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC $BLPrefs::LastRowPos;
		extent = $BLPrefs::BaseRowWidth SPC 40;
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = (($BLPrefs::PrefRows % 2) ? "225 225 225" : "240 240 240") SPC "255";
		isRow = 1;
	};

	%label = new GuiTextCtrl(BLPrefRowLabel) {
		profile = "BLPrefTextProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "16 10";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		text = %title;
		maxLength = "255";
	};
	%row.add(%label);

	%extent = getWord(%label.extent, 0);

	%input = new GuiTextEditCtrl(BLPrefTextInput) {
		profile = "BLPrefTextEditProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = (%extent+32) SPC "10";
		extent = ($BLPrefs::BaseRowWidth-%extent-32) SPC "18";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		maxLength = ((getField(%params, 0) $= "") ? 255 : getField(%params, 0));
		historySize = "0";
		password = "0";
		tabComplete = "0";
		sinkAllKeyEvents = "0";
		variableModified = %variable;
	};
	%input.setValue(%value);
	%input.schedule(0, fixPosExt, %label);

	%row.add(%input);

	%this.add(%row);

	$BLPrefs::LastRowPos += 40;
	$BLPrefs::PrefRows++;
}

function BLPrefPrefList::addCheckboxInput(%this, %title, %variable, %value) {
	// merge commands soon

	%row = new GuiSwatchCtrl(BLPrefTextRow) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "0" SPC $BLPrefs::LastRowPos;
		extent = $BLPrefs::BaseRowWidth SPC 40;
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = (($BLPrefs::PrefRows % 2) ? "225 225 225" : "240 240 240") SPC "255";
		isRow = 1;
	};

	%input = new GuiCheckBoxCtrl(BLPrefCheckboxInput) {
		profile = "BLPrefCheckBoxProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "16 10";
		extent = "140 17";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		text = %title;
		groupNum = "-1";
		buttonType = "ToggleButton";
	};

	%input.setValue(%value);

	%row.add(%input);

	%this.add(%row);

	$BLPrefs::LastRowPos += 40;
	$BLPrefs::PrefRows++;
}

function BLPrefTextInput::fixPosExt(%this, %label) {
	%extent = getWord(%label.extent, 0);
	%this.resize(%extent + 32, 10, $BLPrefs::BaseRowWidth - %extent - 48, 18);
}

function clientCmdFinishReceivePref() {
	BLPrefPrefList.extent = getWord(BLPrefPrefList.extent, 0) SPC $BLPrefs::LastRowPos;
	%params = BLPrefPrefListScroll.position SPC BLPrefPrefListScroll.extent;
	BLPrefPrefListScroll.resize(getWord(%params, 0), getWord(%params, 1), getWord(%params, 2), getWord(%params, 3));
}