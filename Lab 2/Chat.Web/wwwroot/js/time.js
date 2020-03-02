function fromServer(timestamp) {
    var t1 = new Date(timestamp.toString());
    var t2 = new Date(Date.now().toString());
    var str = "";

    if (t1.getTime() < t2.getTime()) {
        str += t1.getDate() + "/" + t1.getMonth() + "/" + t1.getFullYear() + " ";
    }

    str += t1.getHours() + ":" + t1.getMinutes() + ":" + t1.getSeconds();
    return str;
}