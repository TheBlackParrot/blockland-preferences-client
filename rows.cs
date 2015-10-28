function BLPrefPrefList::addTextInput(%this, %title, %variable, %value, %params) {
	%row = new GuiSwatchCtrl(BLPrefTextRow) {
		profile = "GuiDefaultProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = "1" SPC $BLPrefs::LastRowPos;
		extent = "348 40";
		minExtent = "8 2";
		enabled = "1";
		visible = "1";
		clipToParent = "1";
		color = (($BLPrefs::PrefRows % 2) ? "225 225 225" : "240 240 240") SPC "255";
		isRow = 1;
	};

	%label = new GuiTextCtrl(BLPrefRowLabel) {
		profile = "GuiTextProfile";
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
	echo(%extent);

	%input = new GuiTextEditCtrl(BLPrefTextInput) {
		profile = "GuiTextEditProfile";
		horizSizing = "right";
		vertSizing = "bottom";
		position = (%extent+32) SPC "10";
		extent = (348-%extent-32) SPC "18";
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

function BLPrefTextInput::fixPosExt(%this, %label) {
	%extent = getWord(%label.extent, 0);
	%this.resize(%extent + 32, 10, 348 - %extent - 48, 18);
}