function sendVote(id, positive) {
        $.ajax({
            type: "POST",
            url: "/Main/Vote",
            data: {
                isPositive: positive,
                eventId: id
            }
        });
}

function loadNext() {

}