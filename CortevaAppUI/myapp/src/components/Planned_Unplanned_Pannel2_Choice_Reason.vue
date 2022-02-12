
<template>
  <div >
    <div align="center" class="productionName rcorners2">
      <template v-if="downtimeType === 'unplannedDowntime'">
        {{$t("unplannedDowntime")}}
      </template>
      <template v-else>
        {{$t("plannedDowntime")}}
      </template>
    </div>

    <br/>


    <div class="row" align="center">

      <div class="col-sm-4" v-for="reason in events" :key="reason.id">
        <button
            class="btn btn-primary border-info btn-lg btn-block align-items-center btn-info"
            type="button" @click.prevent="setReasonDowntime($t(reason.reason))">
          {{$t(reason.reason)}}
        </button>

      </div>



    </div>


    <br/>

    <div align="left">
      <button type="button" class="btn btn-danger" @click.prevent="backPage()">
        {{$t("back")}}
      </button>
    </div>
  </div>
</template>

<script>
import router from "@/router";
import {default as axios} from "axios";
const urlAPI = 'http://localhost:13197/api/';

export default {
  name: "Unplanned_Pannel_Choice",
  data(){
    return {
      url : sessionStorage.getItem("url"),
      productionName : sessionStorage.getItem("productionName"),
      downtimeType : sessionStorage.getItem("downtimeType"),
      parameters : [],
      events: null,
    }
  },

  methods:{

    setReasonDowntime : function (reasonDowntime) {

      if(sessionStorage.getItem("reasonDowntime") === null){
        sessionStorage.reasonDowntime = reasonDowntime;
      }else{
        sessionStorage.setItem("reasonDowntime",reasonDowntime);
      }

      switch (reasonDowntime) {

        case this.$t("CIP"):
          router.replace("/eventDeclaration/"+this.productionName+"/"+this.downtimeType+"/CIP");
          break;

        case this.$t("formatChanging"):
          router.replace("/eventDeclaration/"+this.productionName+"/"+this.downtimeType+"/formatChanging");
          break;

        case this.$t("packNumberChanging"):
          router.replace("/eventDeclaration/"+this.productionName+"/"+this.downtimeType+"/clientChanging");
          break;

        case this.$t("unplannedDowntime"):
          router.replace("/eventDeclaration/"+this.productionName+"/"+this.downtimeType+"/unplannedDowntime");
          break;

        default :
          router.replace("/eventDeclaration/"+this.productionName+"/"+this.downtimeType+"/"+reasonDowntime);
          break;

      }



    },

    backPage : function () {
      router.replace("/eventDeclaration/"+this.productionName);
    }


  },

  async mounted() {

    if (sessionStorage.getItem("language") !== null) {
      this.$i18n.locale = sessionStorage.getItem("language");
    }
    console.log('ICI : ' + this.downtimeType);
    this.parameters.push(this.productionName);
    this.parameters.push(this.downtimeType);

    await axios.get(urlAPI + 'summary/' + this.productionName + '/' + this.downtimeType)
        .then(response => (this.events = response.data))



  },
}
</script>

<style scoped>
.productionName{
  left:0;
  top:0;
  min-width: 150px;
  max-width: 250px;

  margin-bottom: 50px;
}
.rcorners1{
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

#summary{
  padding: 40px;
}

button{
  color: white;
  margin-top: 20px;
}
</style>