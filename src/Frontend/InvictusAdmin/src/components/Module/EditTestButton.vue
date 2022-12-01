<template>
  <q-btn :class="$attrs.class" label="Редактировать тест" @click="openDialog" />

  <q-dialog v-model="editTestDialog">
    <q-card-section class="q-pt-none">
        <q-card>
      <q-card-section>
        <div class="text-h6 text-center">Добавить тест к уроку "{{ article.title }}" </div>
      </q-card-section>
      <q-form class="q-gutter-md" @submit="onSubmit" @reset="onReset">
        <q-card-section>

          <q-input dense v-model="test.testShowCount"  type="number" label="Количество вопросов для пользователя" />
          <q-input dense v-model="test.testCompleteCount"  type="number" label="Количество вопросов для успешного прохождения" />

          <div  v-for="(input, index) in test.testQuestions" :key="`questionInput-${index}`">

            <div class="text-h8 text-left"> Выберите тип вопроса </div>
            <select v-model="input.questionType">
                <option v-for="t in types" v-bind:value="{ num: t.num, text: t.name }">
                  {{ t.name }}
                </option>
            </select>

            <q-input dense v-model="input.question" label="Введите вопрос"
            lazy-rules
            :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]" />

            <div  v-for="(answerInput, index) in input.answers" :key="`answerInput-${index}`">
            <q-input dense v-model="answerInput.text" label="Вариант ответа"
            lazy-rules
            :rules="[
                (val) => (val && val.length > 0) || 'Поле не должно быть пустым'
              ]"/>
            <q-checkbox  v-model="answerInput.isCorrect" size="xs" label="Отметить как правильный" />
          </div>

          <q-card-actions align="left" class="text-primary">
            <q-btn @click="addAnswerField(input.answers)"  label="Добавить ответ" />
            <q-btn @click="removeField(index, input.answers)" label="Удалить ответ" />
          </q-card-actions>

          </div>

          <q-card-actions align="right" class="text-primary">
            <q-btn @click="addField(test.testQuestions)"  label="Добавить вопрос" />
            <q-btn @click="removeField(index, test.testQuestions)" label="Удалить вопрос" />
          </q-card-actions>

        </q-card-section>
        <q-card-actions align="center" class="text-primary">
          <q-btn flat type="submit" label="Сохранить" />
          <q-btn flat type="reset" label="Отмена" />
        </q-card-actions>
      </q-form>
    </q-card>
      </q-card-section>

  </q-dialog>
</template>

<script>
import { defineComponent } from "vue";
import { fetchModuleById, addTest } from "boot/axios";
import notify from "boot/notifyes";

export default defineComponent({
  props: {
      order: {
        type: Number,
        required: true,
      },
      id: {
        type: Number,
        required: true,
      },
    },
  data() {
      return {
        editTestDialog: false,
        articles: [],
        article: "",
        test:{
          testShowCount:"",
          testCompleteCount:"",
          testQuestions: [{question: "", questionType: "",  answers:[{text: "", isCorrect: false}]}]
        },
        types: [{num: 0, name: 'Один вариант ответа'}, {num: 1, name: 'Несколько вариaнтов ответа'}],
      };
    },
  methods: {
    async openDialog(){
      this.editTestDialog = true
      const response = await fetchModuleById(this.id);
      this.articles = response.data.value.articles
      this.article = this.articles.find(a => a.order === this.order)
      this.test = this.article.test
    },
    async onSubmit() {
      this.test.testQuestions.forEach(element => {
        element.questionType = element.questionType.num
      })
      console.log(this.test)
      try {
        let payload = {
        moduleId: this.id,
        order: this.order,
        test: this.test
      }
        console.log(payload)
        const response = await addTest(payload);
        console.log(response)

        if(response.data.isSuccess){
          this.editTestDialog = false
          this.$emit("updateTest");
          notify.showSucsessNotify("Тест успешно обновлен");
        }
        else{
          response.data.errors.forEach(element => { notify.showErrorNotify(element); });
        }
      } catch (e) {
        notify.showErrorNotify(e.message);
      }
    },
    onReset() {
      this.editTestDialog = false;
      this.errorMessage = "";
    },
   addField(fieldType) {
      fieldType.push({question: "", questionType: "",  answers:[{text: "", isCorrect: false}]});
    },
    removeField(index, fieldType) {
      fieldType.splice(index, 1);
    },
    addAnswerField(fieldType) {
      fieldType.push({text: "", isCorrect: false});
    },
  },
});
</script>
