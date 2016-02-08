function refreshGrid(id) {
    var parameters = { "ClassroomId": id };
    $.ajax({
        type: "POST",
        url: "../../Classrooms/SeatGrid",
        data: JSON.stringify(parameters),
        contentType: "application/json",
        datatype: "html",
        error: function () { alert("Error updating Status!!"); },
        success: function (result) {
            var TagHtml = "";
            for (var i = 0 ; i<result.length; i++) {
                var item = result[i];
                TagHtml = TagHtml + "<span style='float:left' class='remotegrid'><table>";
                TagHtml = TagHtml + "<tr><th colspan='5'>" + item.User.EmailAddress + "</th></tr>";
                if (item.SessionId != "") {
                    TagHtml = TagHtml + "<tr><td colspan='5'><img src='" + item.Thumbnail + "' title = 'Join Session'/></td></tr>"
                }
                else {
                    TagHtml = TagHtml + "<tr><td colspan='5'>This space<br/>Intentionally<br/>Left Blank!</td></tr>";
                }
//                TagHtml = TagHtml + "<tr><td colspan='5'>" + machinelist + "</th></tr>";
                TagHtml = TagHtml + "<tr><td><a onclick='Power(" + item.SeatId + ",findMachine($(this)))'><img src='../../Images/016836-3d-glossy-blue-orb-icon-symbols-shapes-power-button.png'style='width:48px; height:48px'/></a></td>";
                TagHtml = TagHtml + "<td><img src='../../Images/016848-3d-glossy-blue-orb-icon-symbols-shapes-shape-stop-button.png'style='width:48px; height:48px'/></td>";
                TagHtml = TagHtml + "<td><a onclick='Pause(" + item.SeatId + ",findMachine($(this)))'><img src='../../Images/016848-3d-glossy-blue-orb-icon-symbols-shapes-shape-pause-button.png'style='width:48px; height:48px'/></a></td>";
                TagHtml = TagHtml + "<td><a onclick='Start(" + item.SeatId + ",findMachine($(this)))'><img src='../../Images/004326-3d-glossy-blue-orb-icon-arrows-triangle-circle-right.png'style='width:48px; height:48px'/></a></td>";
                TagHtml = TagHtml + "<td><img src='../../Images/075728-3d-glossy-blue-orb-icon-business-computer-monitor.png'style='width:48px; height:48px'/></td></tr>";
                TagHtml = TagHtml + "</table></span>";
            }
            $("#grid").html(TagHtml);
            //setTimeout(refreshGrid(id), 30000);
        }
    });
};