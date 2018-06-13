function sendVote(eventId, sessionId, positive, nextId) {
    $.ajax({
            type: "POST",
            url: "/Interaction/Vote",
            data: {
                isPositive: positive,
                eventId: eventId,
                sessionId: sessionId
            }
        });
    $("#" + eventId).hide();
    loadNext(nextId);
}

function skip(currentId, nextId) {
    $("#" + currentId).hide();
    loadNext(nextId);
}

function loadNext(nextId) {
    if (!nextId)
        $("#voteEnd").show();
    else
        $("#" + nextId).show();
}