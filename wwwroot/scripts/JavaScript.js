function copyUrl(leagueID) {
    var dummy = document.createElement('input'),
        text = window.location.href;

    if (!text.includes('?')) {
        text = text + "?LeagueID=" + leagueID;
    }

    document.body.appendChild(dummy);
    dummy.value = text;

    dummy.select();
    document.execCommand('copy');
    document.body.removeChild(dummy);

    alert("URL copied to clipboard.")
}