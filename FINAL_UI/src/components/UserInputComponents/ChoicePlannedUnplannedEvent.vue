<template>
  <div>
    <div align="center" class="productionName rcorners2">
      {{productionName}}
    </div>

    <br/>

    <div class="row" align="center">
      <div class="col">
        <button
            class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
            type="button" @click.prevent="setDowntimeType('plannedDowntime')">
          {{$t("plannedDowntime")}}
        </button>
      </div>

      <div class="col">

        <button
            class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
            type="button" @click.prevent="setDowntimeType('unplannedDowntime')">
          {{$t("unplannedDowntime")}}
        </button>

      </div>
    </div>
    <br/>


    <div align="center" class="rcorners2 table-info-data" id="summary">
      <table class="table">
        <thead>
        <tr>
          <th scope="col">{{$t("type")}}</th>
          <th scope="col">{{$t("entryTime")}}</th>
          <th scope="col">{{$t("duration(Minutes)")}}</th>
          <th scope="col">{{$t("comments")}}</th>

        </tr>
        </thead>
        <tbody>
          <tr v-for="event in arrayEvents" :key="event.id">
            <th scope="row">{{$t(event.type)}}</th>
            <td>{{event.updated_at.split(' ')[1]}}</td>
            <td>{{event.total_duration}}</td>
            <td>{{event.comment}}</td>
          </tr>
        </tbody>
      </table>
    </div>

    <br/>


    <div align="left">
      <button type="button" class="btn btn-danger" @click.prevent="backPage()">{{$t("back")}}</button>
    </div>
  </div>
</template>

<script>
import router from "@/router";
import {default as axios} from "axios";
import { urlAPI} from "@/variables";

export default {
  name: "ChoicePlannedUnplannedEvent",

  data() {
    return {
      url: sessionStorage.getItem("url"),
      productionName: sessionStorage.getItem("productionName"),
      prodlines: sessionStorage.getItem("prodlines").split(','),
      indice: 0,
      productionlines: sessionStorage.getItem("prodlines").split(','),

      events : sessionStorage.getItem("eventsPerProductionline"),

      PO: sessionStorage.getItem("pos").split(','),

      eventsPerProductionline: [],
      eventsUser: null,
      arrayEvents : null,

    }
  },

  methods: {

    setDowntimeType: function (downtimeType) {

      if (sessionStorage.getItem("downtimeType") === null) {
        sessionStorage.downtimeType = downtimeType;
      } else {
        sessionStorage.setItem("downtimeType", downtimeType);
      }
      router.replace(this.productionName+"/"+downtimeType);

    },

    backPage: function () {
      router.replace('/homePage');
    },
    resolveAfter15Second: function () {
      return new Promise(resolve => {
        setTimeout(() => {
          resolve('resolved');
        }, 1000);
      });


    },


  },

  async mounted() {

    if (sessionStorage.getItem("language") !== null) {
      this.$i18n.locale = sessionStorage.getItem("language");
    }

    for (let i = 0; i < this.productionlines.length; i++) {

      this.eventsPerProductionline.push(this.productionlines[i]);

      this.eventsPerProductionline[this.eventsPerProductionline[i]] = [];

      await axios.get(urlAPI + 'events/' + this.PO[i] + '/' + this.productionlines[i])
          .then(response => (this.eventsUser = response.data))

      await this.resolveAfter15Second();




      for (let j = 0; j < this.eventsUser.length; j++) {
        for (let k = 0; k < this.eventsUser[j].length; k++) {
          this.eventsPerProductionline[this.productionlines[i]].push(this.eventsUser[j][k]);
        }
      }

    }
    console.log(this.eventsPerProductionline);
    this.arrayEvents = this.eventsPerProductionline[this.productionName];



  }

}
</script>

<style scoped>
.productionName {
  left: 0;
  top: 0;
  max-width: 150px;
  margin-bottom: 50px;
}

.rcorners1 {
  border-radius: 25px;
  background: lightblue;
  padding: 20px;
  margin-bottom: 30px;
  width: 180px;

}


.rcorners2 {
  border-radius: 25px;
  border: 2px solid lightblue;
  padding: 20px;
}

#summary {
  padding: 40px;
}

button {
  color: white;
  margin-top: 20px;
}

.table-info-data {
  overflow:scroll; max-height: 300px;
}

thead {
  color:white;
  background: #56baed;
}

</style>