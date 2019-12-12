(function () {
    appModule.controller('common.views.auditLogs.detailModal', [
        '$scope', '$uibModalInstance', 'auditLog', 'abp.services.app.auditLog',
        function ($scope, $uibModalInstance, auditLog, auditLogService) {
            var vm = this;

            vm.auditLog = auditLog;

            vm.getExecutionTime = function() {
                return moment(vm.auditLog.executionTime).fromNow() + ' (' + moment(vm.auditLog.executionTime).format('YYYY-MM-DD HH:mm:ss') + ')';
            };

            vm.getDurationAsMs = function() {
                return app.localize('Xms', vm.auditLog.executionDuration);
            };

            vm.getFormattedParameters = function () {
                try {
                    var json = JSON.parse(vm.auditLog.parameters);
                    return JSON.stringify(json, null, 4);
                } catch (e) {
                    return vm.auditLog.parameters;
                }
            };
            vm.DesSData = {};
            vm.DesSercrt = function(){
                var json = JSON.parse(vm.auditLog.parameters);
                if (json.input.data && json.input.sign && json.input.cid) {
                    console.log(json.input.data);
                    auditLogService.desSercrt(json.input).then(function (result) {
                        vm.DesSData = result.data;
                    });                   
                } else {
                    abp.message.error("参数无需解密！");
                }

            };

            vm.getFormatted = function (data) {
                try {
                    var json = JSON.parse(data);
                    return JSON.stringify(json, null, 4);
                } catch (e) {
                    return data;
                }
            };

            vm.close = function () {
                $uibModalInstance.dismiss();
            };
        }
    ]);
})();