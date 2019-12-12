(function () {
    appModule.controller('tenant.views.dashboard.modifyArr.index', [
        '$scope', 'abp.services.app.receiveJson', '$state', '$uibModal', 'uiGridConstants', '$uibModalInstance','Aid',
        function ($scope, receiveJson, $state, $uibModal, uiGridConstants, $uibModalInstance,Aid) {
            var vm = this;
            vm.loading = false;

            $scope.$on('$viewContentLoaded', function () {
                App.initAjax(); 
            }); 

            vm.cancel = function () {
                $uibModalInstance.dismiss();
            };
            vm.turnPlates = [];
            vm.reasions = [];
            vm.chooseReasions = [];
            vm.chooseTime;
            vm.paramters = {
                aid: Aid,
                playCommand: "",
                checkInCode: "",
                gateCode: "",
                turnPlateCode: "",
                reasonCode: "",
                arrOrDepTime:""
            }

            vm.Init = function () {
                vm.loading = true;
                receiveJson.getAllTurnPlate()
                    .then(function (result) {
                        vm.turnPlates = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
                receiveJson.getAllReason()
                    .then(function (result) {
                        vm.reasions = result.data;
                    }).finally(function () {
                        vm.loading = false;
                    });
            };

            vm.play = function(){
                if (!vm.paramters.playCommand) {
                    abp.message.warn("请选择具体播放内容！");
                    return;
                }
                if (vm.paramters.playCommand == 'FBAG' && !vm.paramters.turnPlateCode) {
                    abp.message.warn("请选择行李转盘！");
                    return;
                }
                if (vm.paramters.playCommand == 'DLY_J' && (!vm.chooseReasions || vm.chooseReasions.length==0)) {
                    abp.message.warn("请选择延误原因！");
                    return;
                }
                if (vm.paramters.playCommand == 'DLY_J' && (!vm.chooseReasions || vm.chooseReasions.length == 0)) {
                    abp.message.warn("请选择取消原因！");
                    return;
                }
                vm.paramters.reasonCode = vm.chooseReasions[0];

                if (vm.chooseTime) {
                    console.log(vm.chooseTime);
                    vm.paramters.arrOrDepTime = new moment(vm.chooseTime).format("HH:mm");
                } else {
                    vm.paramters.arrOrDepTime = null;
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