(function () {
    appModule.controller('tenant.views.dashboard.modifyCheckIn.index', [
        '$scope', 'abp.services.app.receiveJson', '$state', '$uibModal', 'uiGridConstants', '$uibModalInstance', 'Aid',
        function ($scope, receiveJson, $state, $uibModal, uiGridConstants, $uibModalInstance,Aid) {
            var vm = this;
            vm.loading = false;
            vm.paramters = {
                aid: Aid,
                playCommand: "",
                checkInCode: "",
                gateCode: "",
                turnPlateCode: "",
                reasonCode: "",
                arrOrDepTime: ""
            }
            $scope.$on('$viewContentLoaded', function () {
                App.initAjax(); 
            }); 

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.checkins = [];
            vm.Init = function () {
                vm.loading = true;
                receiveJson.getAllCheckIn()
                    .then(function (result) {
                        vm.checkins = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });

            };
            vm.play = function () {
                if (!vm.paramters.playCommand) {
                    abp.message.warn("请选择具体播放内容！");
                    return;
                }
                if (vm.paramters.playCommand != 'CKOFF' && !vm.paramters.checkInCode) {
                    abp.message.warn("请选择行李转盘！");
                    return;
                }
                 

                console.log(vm.paramters);
                receiveJson.handPlay(vm.paramters).then(succ => {
                    abp.notify.success("播放成功！");
                });

            };


            vm.Init();
        }
    ]);
})();