(function () {
    appModule.controller('common.views.audioSets.playSet.EditDevices', [
        '$scope', '$uibModalInstance', 'FileUploader', 'abp.services.app.audioPlaySet', 'entityId',
        function ($scope, $uibModalInstance, fileUploader, audioPlaySet, entityId) {
            var vm = this;

            vm.saving = false;
            vm.save = function () {
                vm.saving = true;
                var ids = [];
                if (vm.entity) {
                    for (var i = 0; i < vm.entity.length; i++) {                        
                        if (vm.entity[i].isChoose) {
                            ids.push(vm.entity[i].id);
                        }                     
                    }

                    var paramters = {
                        id: entityId,
                        dids: ids
                    };

                    audioPlaySet.updateDeviceList(paramters).then(function () {
                        abp.notify.info(app.localize('SavedSuccessfully'));
                        $uibModalInstance.close();
                    }).finally(function () {
                        vm.saving = false;
                    });
                }
            };

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };


            function init() {
                if (entityId) {
                    audioPlaySet.getDeviceList({
                        id: entityId
                    }).then(function (result) {
                        vm.entity = result.data;
                        console.log(vm.entity);
                    });
                }
            }


            init();
        }
    ]);
})();