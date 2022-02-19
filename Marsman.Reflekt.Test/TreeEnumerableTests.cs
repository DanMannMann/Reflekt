using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Marsman.Reflekt.Test
{

    [TestClass]
	public class TreeEnumerableTests
	{
		// TODO need to put some actual tests in here

		private NotNode simpleTree = new NotNode
		{
			InterfaceNode = new SpecialNode
			{
				ClassNode = new SpecialNode
				{
					ObjectNode = 69
				}
			}
		};

		private NotNode tree = new NotNode
		{
			InterfaceNode = new SpecialNode
			{
				ClassNode = new SpecialNode
				{
					InterfaceNode = new Node
					{
						ObjectNode = new Node
						{
							ObjectNode = new string[] { "dave", "biihghg" }
						}
					},
					ObjectNode = 69
				},
				ObjectNode = new NotNode
				{
					InterfaceNode = new Node
					{
						InterfaceNode = new SpecialNode2
						{
							ObjectNode = "asshole"
                        }
                    }
                }
			},
			ClassNode = new Node
			{
				ClassNode = new Node
				{
					InterfaceNode = new Node
					{
						ClassNode = new Node
						{
							InterfaceNode = new SpecialNode
							{
								ClassNode = new SpecialNode
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "biihghg" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new NotNode
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new SpecialNode2
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ClassNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new SpecialNode2
										{
											ObjectNode = new string[] { "reeeg", "steve" }
										}
									},
									ObjectNode = new Node
									{
										InterfaceNode = new Node
										{
											ClassNode = new Node
											{
												InterfaceNode = new Node
												{
													ObjectNode = new Node
													{
														ObjectNode = new string[] { "dave", "guu" },
														ClassNode = new Node
                                                        {
															ObjectNode = new NotNode
															{
																InterfaceNode = new SpecialNode
																{
																	ClassNode = new SpecialNode
																	{
																		InterfaceNode = new Node
																		{
																			ObjectNode = new Node
																			{
																				ObjectNode = new string[] { "dave", "biihghg" }
																			}
																		},
																		ObjectNode = 69
																	},
																	ObjectNode = new NotNode
																	{
																		InterfaceNode = new Node
																		{
																			InterfaceNode = new SpecialNode2
																			{
																				ObjectNode = "asshole"
																			}
																		}
																	}
																},
																ClassNode = new Node
																{
																	ClassNode = new Node
																	{
																		InterfaceNode = new Node
																		{
																			ClassNode = new Node
																			{
																				InterfaceNode = new SpecialNode
																				{
																					ClassNode = new SpecialNode
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "biihghg" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new NotNode
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new SpecialNode2
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ClassNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new SpecialNode2
																							{
																								ObjectNode = new string[] { "reeeg", "steve" }
																							}
																						},
																						ObjectNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ClassNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										ObjectNode = new Node
																										{
																											ObjectNode = new string[] { "dave", "guu" }
																										}
																									},
																									ObjectNode = 69
																								},
																								ObjectNode = new NotNode
																								{
																									InterfaceNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = "asshole"
																										}
																									}
																								}
																							},
																							ClassNode = new Node
																							{
																								ClassNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										ObjectNode = new Node
																										{
																											ObjectNode = new string[] { "dave", "steve" }
																										}
																									},
																									ObjectNode = 69
																								},
																								ObjectNode = new Node
																								{
																									InterfaceNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = "asshole"
																										}
																									}
																								}
																							},
																							ObjectNode = "test"
																						}
																					},
																					ObjectNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ObjectNode = "test"
																			},
																			ObjectNode = new SpecialNode2
																			{
																				ObjectNode = new string[] { "reeeg", "steve" }
																			}
																		},
																		ObjectNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						ObjectNode = new Node
																						{
																							ObjectNode = new string[] { "dave", "guu" }
																						}
																					},
																					ObjectNode = 69
																				},
																				ObjectNode = new NotNode
																				{
																					InterfaceNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = "asshole"
																						}
																					}
																				}
																			},
																			ClassNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						ObjectNode = new Node
																						{
																							ObjectNode = new string[] { "dave", "steve" }
																						}
																					},
																					ObjectNode = 69
																				},
																				ObjectNode = new Node
																				{
																					InterfaceNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = "asshole"
																						}
																					}
																				}
																			},
																			ObjectNode = "test"
																		}
																	},
																	ObjectNode = new Node
																	{
																		InterfaceNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ObjectNode = "asshole"
																			}
																		}
																	}
																},
																ObjectNode = new NotNode
																{
																	InterfaceNode = new SpecialNode
																	{
																		ClassNode = new SpecialNode
																		{
																			InterfaceNode = new Node
																			{
																				ObjectNode = new Node
																				{
																					ObjectNode = new string[] { "dave", "biihghg" }
																				}
																			},
																			ObjectNode = 69
																		},
																		ObjectNode = new NotNode
																		{
																			InterfaceNode = new Node
																			{
																				InterfaceNode = new SpecialNode2
																				{
																					ObjectNode = "asshole"
																				}
																			}
																		}
																	},
																	ClassNode = new Node
																	{
																		ClassNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				ClassNode = new Node
																				{
																					InterfaceNode = new SpecialNode
																					{
																						ClassNode = new SpecialNode
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = new Node
																								{
																									ObjectNode = new string[] { "dave", "biihghg" }
																								}
																							},
																							ObjectNode = 69
																						},
																						ObjectNode = new NotNode
																						{
																							InterfaceNode = new Node
																							{
																								InterfaceNode = new SpecialNode2
																								{
																									ObjectNode = "asshole"
																								}
																							}
																						}
																					},
																					ClassNode = new Node
																					{
																						ClassNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = new SpecialNode2
																								{
																									ObjectNode = new string[] { "reeeg", "steve" }
																								}
																							},
																							ObjectNode = new Node
																							{
																								InterfaceNode = new Node
																								{
																									ClassNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = new Node
																											{
																												ObjectNode = new string[] { "dave", "guu" }
																											}
																										},
																										ObjectNode = 69
																									},
																									ObjectNode = new NotNode
																									{
																										InterfaceNode = new Node
																										{
																											InterfaceNode = new Node
																											{
																												ObjectNode = "asshole"
																											}
																										}
																									}
																								},
																								ClassNode = new Node
																								{
																									ClassNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											ObjectNode = new Node
																											{
																												ObjectNode = new string[] { "dave", "steve" }
																											}
																										},
																										ObjectNode = 69
																									},
																									ObjectNode = new Node
																									{
																										InterfaceNode = new Node
																										{
																											InterfaceNode = new Node
																											{
																												ObjectNode = "asshole"
																											}
																										}
																									}
																								},
																								ObjectNode = "test"
																							}
																						},
																						ObjectNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								InterfaceNode = new Node
																								{
																									ObjectNode = "asshole"
																								}
																							}
																						}
																					},
																					ObjectNode = "test"
																				},
																				ObjectNode = new SpecialNode2
																				{
																					ObjectNode = new string[] { "reeeg", "steve" }
																				}
																			},
																			ObjectNode = new Node
																			{
																				InterfaceNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "guu" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new NotNode
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ClassNode = new Node
																				{
																					ClassNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							ObjectNode = new Node
																							{
																								ObjectNode = new string[] { "dave", "steve" }
																							}
																						},
																						ObjectNode = 69
																					},
																					ObjectNode = new Node
																					{
																						InterfaceNode = new Node
																						{
																							InterfaceNode = new Node
																							{
																								ObjectNode = "asshole"
																							}
																						}
																					}
																				},
																				ObjectNode = "test"
																			}
																		},
																		ObjectNode = new Node
																		{
																			InterfaceNode = new Node
																			{
																				InterfaceNode = new Node
																				{
																					ObjectNode = "asshole"
																				}
																			}
																		}
																	},
																	ObjectNode = "test"
																}
															}
														}
													}
												},
												ObjectNode = 69
											},
											ObjectNode = new NotNode
											{
												InterfaceNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = "asshole"
													}
												}
											}
										},
										ClassNode = new Node
										{
											ClassNode = new Node
											{
												InterfaceNode = new Node
												{
													ObjectNode = new Node
													{
														ObjectNode = new string[] { "dave", "steve" }
													}
												},
												ObjectNode = 69
											},
											ObjectNode = new Node
											{
												InterfaceNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = "asshole"
													}
												}
											}
										},
										ObjectNode = "test"
									}
								},
								ObjectNode = new Node
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ObjectNode = "test"
						},
						ObjectNode = new SpecialNode2
						{
							ObjectNode = new string[] { "reeeg", "steve" }
						}
					},
					ObjectNode = new Node
					{
						InterfaceNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new Node
								{
									ObjectNode = new Node
									{
										ObjectNode = new string[] { "dave", "guu" }
									}
								},
								ObjectNode = 69
							},
							ObjectNode = new NotNode
							{
								InterfaceNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = "asshole"
									}
								}
							}
						},
						ClassNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new Node
								{
									ObjectNode = new Node
									{
										ObjectNode = new string[] { "dave", "steve" }
									}
								},
								ObjectNode = 69
							},
							ObjectNode = new Node
							{
								InterfaceNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = "asshole"
									}
								}
							}
						},
						ObjectNode = "test"
					}
				},
				ObjectNode = new Node
				{
					InterfaceNode = new Node
					{
						InterfaceNode = new Node
						{
							ObjectNode = "asshole"
						}
					}
				}
			},
			ObjectNode = new NotNode
			{
				InterfaceNode = new SpecialNode
				{
					ClassNode = new SpecialNode
					{
						InterfaceNode = new Node
						{
							ObjectNode = new Node
							{
								ObjectNode = new string[] { "dave", "biihghg" }
							}
						},
						ObjectNode = 69
					},
					ObjectNode = new NotNode
					{
						InterfaceNode = new Node
						{
							InterfaceNode = new SpecialNode2
							{
								ObjectNode = "asshole"
							}
						}
					}
				},
				ClassNode = new Node
				{
					ClassNode = new Node
					{
						InterfaceNode = new Node
						{
							ClassNode = new Node
							{
								InterfaceNode = new SpecialNode
								{
									ClassNode = new SpecialNode
									{
										InterfaceNode = new Node
										{
											ObjectNode = new Node
											{
												ObjectNode = new string[] { "dave", "biihghg" }
											}
										},
										ObjectNode = 69
									},
									ObjectNode = new NotNode
									{
										InterfaceNode = new Node
										{
											InterfaceNode = new SpecialNode2
											{
												ObjectNode = "asshole"
											}
										}
									}
								},
								ClassNode = new Node
								{
									ClassNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = new SpecialNode2
											{
												ObjectNode = new string[] { "reeeg", "steve" }
											}
										},
										ObjectNode = new Node
										{
											InterfaceNode = new Node
											{
												ClassNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = new Node
														{
															ObjectNode = new string[] { "dave", "guu" }
														}
													},
													ObjectNode = 69
												},
												ObjectNode = new NotNode
												{
													InterfaceNode = new Node
													{
														InterfaceNode = new Node
														{
															ObjectNode = "asshole"
														}
													}
												}
											},
											ClassNode = new Node
											{
												ClassNode = new Node
												{
													InterfaceNode = new Node
													{
														ObjectNode = new Node
														{
															ObjectNode = new string[] { "dave", "steve" }
														}
													},
													ObjectNode = 69
												},
												ObjectNode = new Node
												{
													InterfaceNode = new Node
													{
														InterfaceNode = new Node
														{
															ObjectNode = "asshole"
														}
													}
												}
											},
											ObjectNode = "test"
										}
									},
									ObjectNode = new Node
									{
										InterfaceNode = new Node
										{
											InterfaceNode = new Node
											{
												ObjectNode = "asshole"
											}
										}
									}
								},
								ObjectNode = "test"
							},
							ObjectNode = new SpecialNode2
							{
								ObjectNode = new string[] { "reeeg", "steve" }
							}
						},
						ObjectNode = new Node
						{
							InterfaceNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "guu" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new NotNode
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ClassNode = new Node
							{
								ClassNode = new Node
								{
									InterfaceNode = new Node
									{
										ObjectNode = new Node
										{
											ObjectNode = new string[] { "dave", "steve" }
										}
									},
									ObjectNode = 69
								},
								ObjectNode = new Node
								{
									InterfaceNode = new Node
									{
										InterfaceNode = new Node
										{
											ObjectNode = "asshole"
										}
									}
								}
							},
							ObjectNode = "test"
						}
					},
					ObjectNode = new Node
					{
						InterfaceNode = new Node
						{
							InterfaceNode = new Node
							{
								ObjectNode = "asshole"
							}
						}
					}
				},
				ObjectNode = "test"
			}
		};

		[TestMethod]
		public void EnumerateSomeHugeTrees()
		{
			var randomTreeFactory = new HugeRandomTreeFactory(5);
			var tree = randomTreeFactory.Tree;
			var thing = this.tree.AsTreeEnumerableWithContext<object>(0).ToList();

			var allBreadthContextResult = tree.AsTreeEnumerableWithContext<object>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst,
				Filter.ExcludeBranchesAndValues<string>(),
				Filter.ExcludeBranchesAndValues<IEnumerable>(),
				Filter.ExcludeBranchesAndValues<Uri>(),
				Filter.ExcludeBranchesAndValues<int>(),
				Filter.ExcludeValues<NotNode>());

			var allDepthContextResult = tree.AsTreeEnumerableWithContext<object>(
				Filter.ExcludeBranchesAndValues<string>(),
				Filter.ExcludeBranchesAndValues<IEnumerable>(),
				Filter.ExcludeBranchesAndValues<Uri>(),
				Filter.ExcludeBranchesAndValues<int>(),
				Filter.ExcludeValues<NotNode>());

			var sw = Stopwatch.StartNew();
			var g1 = allDepthContextResult.ToList();
			sw.Stop();

            var sw2 = Stopwatch.StartNew();
            var g2 = allBreadthContextResult.ToList();
            sw2.Stop();

            var cnt = randomTreeFactory.Objects.Count(x => x.Object is ITreeNode);
			var b = g1.Max(x => x.Depth);
		}

		[TestMethod]
		public void EnumerateSomeSmallerKnownTrees()
		{
			// add some loops
			(tree.ClassNode.ClassNode.ObjectNode as Node).ObjectNode = tree;

			var firstEmptyStringCollection = tree.AsTreeEnumerable<ICollection<string>>()
											     .FirstOrDefault(x => x.Count() == 0);

			var allBreadthContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst,
				branchingStrategy: TreeBranchingStrategy.AllProperties);

			var l1 = allBreadthContextResult.ToList();
			var l2 = allBreadthContextResult.ToList();


			var specialsResult = simpleTree.AsTreeEnumerableWithContext<INodeWithSpecialThing>(
				branchingStrategy: TreeBranchingStrategy.AllProperties).ToList();
			var specialsResult3 = tree.AsTreeEnumerableWithContext<INodeWithSpecialThing>(
				branchingStrategy: TreeBranchingStrategy.AllProperties, enumerationStrategy: TreeEnumerationStrategy.BreadthFirst).ToList();
			var specialsResult4 = tree.AsTreeEnumerableWithContext<INodeWithSpecialThing>(
				branchingStrategy: TreeBranchingStrategy.AllProperties, enumerationStrategy: TreeEnumerationStrategy.DepthFirst).ToList();

			var propTypeResult = tree.AsTreeEnumerable<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.PropertyTypeIsTvalue).ToList();
			var propTypeContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.PropertyTypeIsTvalue).ToList();

			var propValueResult = tree.AsTreeEnumerable<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.PropertyValueIsTvalue).ToList();
			var propValueContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.PropertyValueIsTvalue).ToList();

			var allResult = tree.AsTreeEnumerable<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.AllProperties).ToList();
			var allContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				branchingStrategy: TreeBranchingStrategy.AllProperties).ToList();


			var propTypeBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchingStrategy: TreeBranchingStrategy.PropertyTypeIsTvalue).ToList();
			var propTypeBreadthContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchingStrategy: TreeBranchingStrategy.PropertyTypeIsTvalue).ToList();

			var propValueBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchingStrategy: TreeBranchingStrategy.PropertyValueIsTvalue).ToList();
			var propValueBreadthContextResult = tree.AsTreeEnumerableWithContext<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchingStrategy: TreeBranchingStrategy.PropertyValueIsTvalue).ToList();

			var allBreadthResult = tree.AsTreeEnumerable<ITreeNode>(
				enumerationStrategy: TreeEnumerationStrategy.BreadthFirst, 
				branchingStrategy: TreeBranchingStrategy.AllProperties).ToList();
		}
	}
}
