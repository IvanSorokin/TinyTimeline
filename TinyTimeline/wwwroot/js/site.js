function sendVote(eventId, sessionId, positive) {
    $.ajax({
            type: 'POST',
            url: '/Interaction/Vote',
            data: {
                isPositive: positive,
                eventId: eventId,
                sessionId: sessionId
            }
        });
    $("#" + eventId).hide();
    checkIfDone();
}

function deleteEvent(eventId, sessionId) {
    $.ajax({
        type: 'DELETE',
        url: '/Interaction/DeleteEvent',
        data: {
            eventId: eventId,
            sessionId: sessionId
        }
    });
    $("#" + eventId).hide();
    checkIfDone();
}

function skip(currentId) {
    $("#" + currentId).hide();
    checkIfDone();
}

function checkIfDone() {
    if ($('.vote_item').filter(":hidden" ).length == $('.vote_item').length)
        $("#voteEnd").show();
}