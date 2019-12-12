(function () {
    appModule.factory('webSocketService', ['$q',
            function ($q) {
                function desktop(obj) {
                    console.log(obj);
                    var deferred = $q.defer();
                    var ws = new WebSocket("ws://127.0.0.1:5412/desktop");

                    ws.onopen = function () {
                        var request;
                        request = {
                            Cmd: "desktop",
                            Content: {
                                Ip: obj.ipAddress,
                                Port: obj.port
                            }
                        };
                        ws.send(JSON.stringify(request));
                    };

                    ws.onmessage = function (evt) {
                        abp.notify.success(evt);
                        //var obj = JSON.parse(evt.data);
                        //if (obj.Error != null && obj.Error.length > 0) {
                        //    abp.notify.error(obj.Error);
                        //}
                        //var received_msg = evt.data;
                        deferred.resolve(evt);
                    };

                    ws.onclose = function () {
                        deferred.reject('连接设备服务失败');
                    };                    ws.onerror = function (evt) {
                        abp.message.error('未能启用WebSocket服务，请先启用读卡程序再试', '连接错误');
                    }
                    return deferred.promise;
                };
                
                return {
                    desktop: desktop
                };
            }
    ]);
})();