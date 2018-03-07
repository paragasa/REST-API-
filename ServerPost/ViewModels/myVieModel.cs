//using System;
//using System.Collections.Generic;
//using System.Text;

//namespace ServerPost.ViewModels
//{
//    class myVieModel
//    {
//        private DogBreed _selectedBreed;
//        public DogBreed SelectedBreed
//        {
//            get
//            {
//                return _selectedBreed;
//            }
//            set
//            {
//                Set(ref _selectedBreed, value);
//            }
//        }

//        public List<string> BreedNames
//        {
//            get
//            {
//                return Enum.GetNames(typeof(DogBreed)).Select(b => b.SplitCamelCase()).ToList();
//            }
//        }

//        public void DoSomethingWithBreed()
//        {
//            DoSomething(SelectedBreed);
//        }
//    }
//}
