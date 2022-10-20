<template>
  <div>
    <h4 style="text-align: center;">Ваши курсы</h4>

    <q-splitter v-model="splitterModel" style="height: 750px">

      <template v-slot:before>
        <q-tabs v-model="tab" vertical class="text-teal">
          <q-tab name="current" icon="mail" label="Текущие" />
          <q-tab name="completed" icon="alarm" label="Завершённые" />
          <q-tab name="wish" icon="movie" label="Желаемые" />
        </q-tabs>
      </template>

      <template v-slot:after>
        <q-tab-panels v-model="tab" animated swipeable vertical transition-prev="jump-up" transition-next="jump-up">
          <q-tab-panel name="current">
            <div class="text-h4 q-mb-md">Текущие</div>
            <course-card />
          </q-tab-panel>

          <q-tab-panel name="completed">
            <div class="text-h4 q-mb-md">Завершённые</div>
            <course-card />
          </q-tab-panel>

          <q-tab-panel name="wish">
            <div class="text-h4 q-mb-md">Желаемые</div>
            <course-card />
          </q-tab-panel>
        </q-tab-panels>
      </template>

    </q-splitter>
  </div>
</template>

<script>
import { ref } from 'vue'
import CourseCard from 'components/CourseCard.vue'

export default {
  props: {
    logined : {
      Type : Boolean
    }
  },
  setup() {
    return {
      tab: ref("current"),
      splitterModel: ref(20)
    };
  },
  mounted(){
    if(!this.logined)
    {
      this.$router.push({ name: 'homepage' })
    }
  },
  components: { CourseCard }
}
</script>

<style lang="scss" scoped>

</style>