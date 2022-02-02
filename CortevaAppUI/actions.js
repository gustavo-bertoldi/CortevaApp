import axios from "axios";

let actions = {
    retrieveToken(context, credentials) {
        axios.post(`/api/log/${credentials[0]}/${credentials[1]}`)
            .then(res => {
                console.log(res);
                var token;
                if (res.data.length > 0) {
                    token = 'OK';
                    localStorage.setItem('access_token', res.data);
                    context.commit('RETREIVETOKEN', res.data);
                }

            }).catch(err => {
            console.log(err)
        })

    },

    fetchUsers({commit}, parameters) {

        axios.get(`/api/users/${parameters[0]}`)
            .then(res => {
                console.log(res.data);
                commit('FETCH_USER', res.data);
            }).catch(err => {
            console.log(err)
        })
    },



    fetchSites({commit}, parameters) {

        axios.get(`/api/sites`)
            .then(res => {
                console.log(res.data);
                commit('FETCH_SITES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },

    fetchMachines({commit}, productionlineID) {
        axios.get(`/api/machines/${productionlineID}`)
            .then(res => {
                commit('FETCH_MACHINES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },

    fetchDowntimeEvents({commit}, parameters) {

        const productionLine = parameters[0];
        const startYear = parameters[1];
        const endYear = parameters[2];

        axios.get(`/api/unplannedDowntimeEvents/${productionLine}/${startYear}/${endYear}`)
            .then(res => {
                commit('FETCH_UNPLANNED_DOWNTIME_EVENTS', res.data);
            }).catch(err => console.log(err));
    },

    fetchMachineShutdownTypes({commit}, parameters) {
        const productionLine = parameters[0];
        const startYear = parameters[1];
        const endYear = parameters[2];

        axios.get(`/api/unplannedDowntimeMachineShutdownTypes/${productionLine}/${startYear}/${endYear}`)
            .then(res => {
                commit('FETCH_UNPLANNED_DOWNTIME_MACHINE_SHUTDOWN_TYPES', res.data);
            }).catch(err => console.log(err));

    },

    fetchAllEvents({commit}, parameters) {

        var site = parameters[0];
        var productionLine = parameters[1];
        var beginningDate = parameters[2];
        var endingDate = parameters[3];

        axios.get(`/api/allevents/${site}/${productionLine}/${beginningDate}/${endingDate}`)
            .then(res => {
                commit('FETCH_ALL_EVENTS', res.data);
            }).catch(err => {
            console.log(err)
        })
    },

    fetchQualityLosses({commit}, parameters) {

        var site = parameters[0];
        var productionLine = parameters[1];
        var beginningDate = parameters[2];
        var endingDate = parameters[3];

        axios.get(`/api/qualityLosses/${site}/${productionLine}/${beginningDate}/${endingDate}`)
            .then(res => {
                commit('FETCH_QUALITY_LOSSES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },

    fetchVolumes({commit}, parameters) {

        var site = parameters[0];
        var productionLine = parameters[1];
        var beginningDate = parameters[2];
        var endingDate = parameters[3];

        axios.get(`/api/allVolumes/${site}/${productionLine}/${beginningDate}/${endingDate}`)
            .then(res => {
                commit('FETCH_ALL_VOLUMES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },



    fetchEvents({commit}, parameters) {

        axios.get(`/api/events/${parameters[1]}/${parameters[2]}`)
            .then(res => {
                //console.log(res.data);
                if (parameters[0] === 1) {
                    commit('FETCH_EVENTS1', res.data);
                } else {
                    commit('FETCH_EVENTS2', res.data);
                }
            }).catch(err => {
            console.log(err)
        })
    },

    fetchPO({commit}, parameters) {
        axios.get(`/api/pos/${parameters[0]}/${parameters[1]}`)
            .then(res => {
                //console.log(res.data);
                commit('FETCH_PO', res.data);
            }).catch(err => {
            console.log(err)
        })
    },


    fetchSpeedLosses({commit}, parameters) {

        axios.get(`/api/speedLosses/${parameters[1]}/${parameters[0]}`)
            .then(res => {
                commit('FETCH_SPEEDLOSSES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },

    getSpeedLosses({commit}, parameters) {
        axios.get(`/api/getSpeedLosses/${parameters[0]}/${parameters[1]}/${parameters[2]}/${parameters[3]}`)
            .then(res => {
                commit('GET_SPEED_LOSSES', res.data);
            }).catch(err => {
            console.log(err)
        })
    },


    fetchDowntimeReason({commit}, parameters) {
        axios.get(`/api/summary/${parameters[0]}/${parameters[1]}`)
            .then(res => {
                console.log(res.data);
                commit('FETCH_DOWNTIME_REASONS', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    fetchDowntimeReason_2({commit}, parameters) {
        axios.get(`/api/${parameters[0]}/${parameters[1]}/unplannedDowntime`)
            .then(res => {
                console.log('Je passe ici');
                console.log(res.data);
                commit('FETCH_DOWNTIME_REASONS_2', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    fetchDowntimeReason_Machine_Issue({commit}, machineName) {
        axios.get(`/api/unplannedDowntime/unplannedDowntime/${machineName}`)
            .then(res => {
                console.log(res.data);
                commit('FETCH_DOWNTIME_REASONS_MACHINE_ISSUE', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    getWorksiteID({commit}, worksite) {
        axios.get(`/api/worksiteID/${worksite}`)
            .then(res => {
                commit('FETCH_WORKSITEID', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    getPerformanceForASite({commit}, PO) {
        axios.get(`/api/performance/${PO}`)
            .then(res => {
                commit('FETCH_PERFORMANCE_INDEXES', res.data)
            }).catch(err => {
            console.log(err)
        })
    },


    getProductionlineID({commit}, productionline) {
        axios.get(`/api/productionlineID/${productionline}`)
            .then(res => {
                commit('FETCH_PRODUCTIONLINEID', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    checkAssignation({commit}, assignation) {

        axios.get(`/api/assignation/${assignation.username}/${assignation.po}/${assignation.productionline}`)
            .then(res => {
                commit('FECTH_ASSIGNATION', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    checkPO({commit}, po) {

        axios.get(`/api/po/${po}`)
            .then(res => {
                commit('FECTH_CHECKPO', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    getNetOPTime({commit}, GMID) {

        axios.get(`/api/netOP/${GMID}`)
            .then(res => {
                commit('FETCH_NETOP', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    create_PO({commit}, potab) {

        for (let i = 0; i < potab.length; i++) {
            axios.post(`/api/PO`, potab[i])
                .then(res => {
                    commit('CREATE_PO', res.data)
                }).catch(err => {
                console.log(err)
            })

        }

    },

    storeAssignation({commit}, assignation) {

        axios.post(`/api/assignation`, assignation)
            .then(res => {
                commit('CREATE_ASSIGNATION', res.data)
            }).catch(err => {
            console.log(err)
        })
    },





    create_UnplannedEvent_Changingformat({commit}, event) {
        axios.post(`/api/unplannedEvent/changingFormat`, event)
            .then(res => {
                commit('CREATE_UNPLANNEDEVENT_CHANGINGFORMAT', res.data)
            }).catch(err => {
            console.log(err)
        })

    },


    create_UnplannedEvent_Clientchanging({commit}, event) {
        axios.post(`/api/unplannedEvent/clientChanging`, event)
            .then(res => {
                commit('CREATE_UNPLANNEDEVENT_CLIENTCHANGING', res.data)
            }).catch(err => {
            console.log(err)
        })

    },


    create_UnplannedEvent_CIP({commit}, event) {
        axios.post(`/api/unplannedEvent/CIP`, event)
            .then(res => {
                commit('CREATE_UNPLANNEDEVENT_CIP', res.data)
            }).catch(err => {
            console.log(err)
        })

    },

    create_UnplannedEvent_UnplannedDowntime({commit}, event) {
        axios.post(`/api/unplannedEvent/unplannedDowntime`, event)
            .then(res => {
                commit('CREATE_UNPLANNEDEVENT_UNPLANNEDDOWNTIME', res.data)
            }).catch(err => {
            console.log(err)
        })
    },

    create_plannedEvent({commit}, event) {
        axios.post(`/api/plannedEvent`, event)
            .then(res => {
                commit('CREATE_PLANNEDEVENT', res.data)
            }).catch(err => {
            console.log(err)
        })

    },

    stop_PO({commit}, array) {
        var PO = array[0];
        var availability =  array[1];
        var performance =  array[2];
        var quality =  array[3];
        var OLE =  array[4];
        var quantityProduced = array[5];
        var totalDuration = array[6];


        axios.post(`/api/stopPO/${PO}/${availability}/${performance}/${quality}/${OLE}/${quantityProduced}/${totalDuration}`, PO)
            .then(res => {
                commit('STOP_PO', res.data)
            }).catch(err => {
            console.log(err)
        })

    },


    store_Rejection({commit}, rejection) {

      /**  var po = array[0];
        var labelerCounter =  array[1];
        var weightBoxCounter =  array[2];
        var caperCounter =  array[3];
        var fillerCounter =  array[4];
        var labelerRejection = array[5];
        var weightBoxRejection = array[6];
        var caperRejection = array[7];
        var fillerRejection = array[8];
        var qualityControlCounter = array[9];
        var qualityControlRejection = array[10];

**/
        axios.post(`/api/storeRejection`, rejection)
            .then(res => {
                commit('STORE_REJECTION', res.data)
            }).catch(err => {
            console.log(err)
        })

    },


    create_SpeedLoss({commit}, event) {
        axios.post(`/api/speedLoss`, event)
            .then(res => {
                commit('CREATE_SPEEDLOSS', res.data)
            }).catch(err => {
            console.log(err)
        })

    },


};

export default actions
