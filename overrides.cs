function getIconOverride(%category, %original) {
	switch$(%category) {
		case "Duplorcator" or "Duplicator": return "wand";
		case "Click Push": return "user_go";
		case "Toggle Suicide": return "user_delete";
	}
	return %original;
}