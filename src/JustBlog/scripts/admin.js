$(function () {
    $("#tabs").tabs({
        show: function (event, ui) {
            if (!ui.tab.isLoaded)
            {
                switch (ui.index) {
                    case 0:
                        // call function to create grid for managing posts
                        // from "#tablePosts" and "#pagerPosts"
                        break;
                    case 1:
                        // call function to create grid for managing posts
                        // from "#tableCats" and "#pagerCats"
                        break;
                    case 2:
                        // call function to create grid for managing posts
                        // from "#tableTags" and "#pagerTags";
                        break;
                };

                ui.tab.isLoaded = true;
            }
        }
    });
});