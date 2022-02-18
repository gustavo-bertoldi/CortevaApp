<template v-if="show===1">

  <div class="row">
    <div v-for="productionline in productionlines" :key="productionline.id" class="col">
          <button
              class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info rcorners1"
              type="button" @click.prevent="setProductionline(productionline)">
            {{productionline}}
          </button>


          <br/>
          <div class="rcorners2">
            <p>
              {{$t("site")}} : {{site}} <br/>
              {{$t("crewLeader")}} : {{crewLeader}} <br/>
              {{$t("typeTeam")}} : {{typeTeam}} <br/>
              {{$t("startTime")}} : {{workingDebut}} <br/>
              {{$t("endTime")}} : {{workingEnd}} <br/>
            </p>
          </div>

          <br/>

          <div class="rcorners1 table-info-data" align="center">
            <table class="table">
              <thead class="thead-dark">
              <tr>
                <th scope="col">{{$t("type")}}</th>
                <th scope="col">{{$t("entryTime")}}</th>
                <th scope="col">{{$t("duration(Minutes)")}}</th>
                <th scope="col">{{$t("comments")}}</th>

              </tr>
              </thead>
              <tbody>
                  <tr v-for="event in eventsPerProductionline[productionline]" :key="event.id">
                    <th scope="row">{{$t(event.type)}}</th>
                    <td>{{event.updated_at.split(' ')[1]}}</td>
                    <td>{{event.total_duration}}</td>
                    <td>{{event.comment}}</td>
                  </tr>
              </tbody>
            </table>


          </div>

          <br/>
    </div>

    <br/>

  </div>
</template>

<script>
import {default as axios} from "axios";
import router from "@/router";
import { urlAPI} from "@/variables";

export default {
  name: "TopSecondPage",

  data() {
    return {
      site: sessionStorage.getItem("site"),
      workingDebut: sessionStorage.getItem("workingDebut"),
      workingEnd: sessionStorage.getItem("workingEnd"),
      productionlines: sessionStorage.getItem("prodlines").split(','),
      username: localStorage.getItem("username"),
      PO: sessionStorage.getItem("pos").split(','),
      GMID: sessionStorage.getItem("GMID").split(','),

      crewLeader: sessionStorage.getItem("crewLeader"),
      typeTeam: sessionStorage.getItem("typeTeam"),
      url: sessionStorage.getItem("url"),

      showSpinner: 0,
      show: 0,
      eventsUser: null,

      worksites :null,

      eventsPerProductionline: [],




    };
  },

  methods: {


    setProductionline: function (productionline) {

      if (sessionStorage.getItem("productionName") === null) {
        sessionStorage.productionName = productionline;
      } else {
        sessionStorage.setItem("productionName", productionline);
      }

      for(let i = 0; i<this.productionlines.length; i++){
        if(this.productionlines[i]===productionline){
          if (sessionStorage.getItem("poNumber") === null) {
            sessionStorage.poNumber = this.PO[i];
          } else {
            sessionStorage.setItem("poNumber", this.PO[i]);
          }
        }
      }




      router.replace('/eventDeclaration/'+productionline);


    },

    resolveAfter05Second: function () {
      return new Promise(resolve => {
        setTimeout(() => {
          resolve('resolved');
        }, 1000);
      });


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
    if(sessionStorage.getItem("language") !== null){
      this.$i18n.locale = sessionStorage.getItem("language");
    }

    console.log(this.productionlines);


    if (this.PO.length > 0 && this.PO[0] !== "") {
      for (let i = 0; i < this.PO.length; i++) {
        var tab = [];
        tab.push(i + 1);
        tab.push(this.PO[i]);
        tab.push(this.productionlines[i]);


        await axios.get(urlAPI + 'events/'+this.PO[i]+'/'+this.productionlines[i])
            .then(response => (this.eventsUser = response.data))


        console.log('OH');

        this.eventsPerProductionline.push(this.productionlines[i]);
        this.eventsPerProductionline[this.eventsPerProductionline[i]] = [];


        for (let j = 0; j < this.eventsUser.length; j++) {
          for (let k = 0; k < this.eventsUser[j].length; k++) {
            this.eventsPerProductionline[this.productionlines[i]].push(this.eventsUser[j][k]);
          }
        }

        console.log(this.eventsPerProductionline);





        await axios.get(urlAPI + 'productionlineID/'+this.productionlines[i])
            .then(response => (this.productionLineID = response.data[0].id))


        await this.resolveAfter05Second();



        var assignation = {
          username: this.username,
          productionline: this.productionLineID,
          po: this.PO[i],
          shift: sessionStorage.getItem("typeTeam"),
          worksite: parseInt(sessionStorage.getItem("worksiteUserID")),
        };

        console.log(assignation);

        var res = null;

        await axios.get(urlAPI+'countassignation/'+this.username+ '/' +this.PO[i]+'/'+ this.productionLineID)
            .then(response => (res = response.data))
        await this.resolveAfter05Second();


        if (res === 0) {
          await axios.post(urlAPI+'addassignation',  {
            username: this.username,
            productionline: this.productionLineID,
            po: this.PO[i],
            shift: sessionStorage.getItem("typeTeam"),
            worksite: parseInt(sessionStorage.getItem("worksiteUserID")),
          })
              .then(response => (res = response.data))
        }

      }

    }

    this.showSpinner =1;
    this.show=1;

    if(sessionStorage.getItem("eventsPerProductionline") !== null){
      sessionStorage.eventsPerProductionline =  this.eventsPerProductionline;
    }else{
      sessionStorage.setItem("eventsPerProductionline", this.eventsPerProductionline);
    }

  },
}
</script>

<style scoped>

</style>