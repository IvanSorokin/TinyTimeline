function sendVote(id, positive, nextId) {
    $.ajax({
            type: "POST",
            url: "/Main/Vote",
            data: {
                isPositive: positive,
                eventId: id
            }
        });
    $("#" + id).hide();
    loadNext(nextId);
}

function loadNext(nextId) {
    if (nextId == '')
        $("#voteEnd").show();
    else
        $("#" + nextId).show();
}